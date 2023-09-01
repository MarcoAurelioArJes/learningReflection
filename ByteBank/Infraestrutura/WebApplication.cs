using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank.Infraestrutura
{
    public class WebApplication
    {
        private readonly string[] _prefixos;
        public WebApplication(string[] prefixos)
        {
            if (prefixos == null)
                throw new ArgumentNullException(nameof(prefixos));

            _prefixos = prefixos;
        }

        public void Iniciar()
        {
            while (true)
                ManipularRequisicao();
        }

        private void ManipularRequisicao()
        {
            var httpListener = new HttpListener();

            foreach (var prefixo in _prefixos)
                httpListener.Prefixes.Add(prefixo);

            httpListener.Start();

            var contexto = httpListener.GetContext();
            var requisicao = contexto.Request;
            var resposta = contexto.Response;

            var caminho = requisicao.Url.AbsolutePath;

            if (caminho == "/Assets/css/styles.css")
            {
                var assembly = Assembly.GetExecutingAssembly();
                var nomeDoRecurso = "ByteBank.Assets.css.styles.css";
                var recursoStream = assembly.GetManifestResourceStream(nomeDoRecurso);
                var recursoEmBytes = new byte[recursoStream.Length];

                recursoStream.Read(recursoEmBytes, 0, recursoEmBytes.Length);

                resposta.StatusCode = 200;
                resposta.ContentType = "text/css charset=utf-8";
                resposta.ContentLength64 = recursoStream.Length;

                resposta.OutputStream.Write(recursoEmBytes, 0, recursoEmBytes.Length);

                resposta.OutputStream.Close();
            }
            else if (caminho == "/Assets/js/main.js")
            {
                var assembly = Assembly.GetExecutingAssembly();
                var nomeDoRecurso = "ByteBank.Assets.js.main.js";
                var recursoStream = assembly.GetManifestResourceStream(nomeDoRecurso);
                var recursoEmBytes = new byte[recursoStream.Length];

                recursoStream.Read(recursoEmBytes, 0, recursoEmBytes.Length);

                resposta.StatusCode = 200;
                resposta.ContentType = "text/javascript charset=utf-8";
                resposta.ContentLength64 = recursoStream.Length;

                resposta.OutputStream.Write(recursoEmBytes, 0, recursoEmBytes.Length);

                resposta.OutputStream.Close();
            }

            httpListener.Stop();
        }
    }
}
