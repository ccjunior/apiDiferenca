using Avaliacao.API.Model;
using Avaliacao.Core.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace Avaliacao.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DiffController : ControllerBase
    {
        private readonly IDistributedCache _distributedCache;

        public DiffController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }


        [HttpGet]
        public ActionResult<Response> Get(Guid id)
        {
            Response resp = new Response();
            var lastData = _distributedCache.GetString(id.ToString());
            var dados = JsonConvert.DeserializeObject<Cache>(lastData);

            if (dados == null)
            {
                resp.Id = id;
                resp.resultado = "Não foram encontradas informações para ser analisadas";
                resp.erro = true;
                return resp;
            }

            if (string.IsNullOrEmpty(dados.dadosWrite) ) 
            {
                resp.Id = id;
                resp.resultado = "Não foi enviado dados para o end point left";
                resp.erro = true;
                return resp;
            }

            if (string.IsNullOrEmpty(dados.dadosLeft))
            {
                resp.Id = id;
                resp.resultado = "Não foi enviado dados para o end point write";
                resp.erro = true;
                return resp;
            }

            if (dados.dadosLeft.Equals(dados.dadosWrite))
            {
                resp.Id = id;
                resp.resultado = "Os dados são idênticos";
                resp.erro = false;
                return resp;
            }
            else
            {
                if (dados.dadosLeft.Length != dados.dadosWrite.Length)
                {
                    resp.Id = id;
                    resp.resultado = "Os dados possuem tamanhos diferentes";
                    resp.erro = true;
                    return resp;
                }
                else
                {

                }
            }

                return resp;
        }


        [HttpPost("left")]
        public ActionResult<Response> left([FromBody] Request req)
        {
            var lastData =  _distributedCache.GetString(req.Id.ToString());

            if(!string.IsNullOrEmpty(lastData))
            {
                var dados = JsonConvert.DeserializeObject<Cache>(lastData);
                dados.dadosLeft = req.Dados.ToString();
            }
            else
            {
                Cache cache = new Cache();
                cache.dadosLeft = req.Dados.ToString();
                _distributedCache.SetString(req.Id.ToString(), cache.dadosLeft);
            }

            Response resp = new Response();
            resp.Id = req.Id;
            resp.resultado = "Informação armazenada com sucesso";
            resp.erro = false;

            return resp;
        }

        [HttpPost("right")]
        public async Task<Response> right([FromBody] Request req)
        {
            var lastData = _distributedCache.GetString(req.Id.ToString());

            if (!string.IsNullOrEmpty(lastData))
            {
                var dados = JsonConvert.DeserializeObject<Cache>(lastData);
                dados.dadosWrite = req.Dados.ToString();
            }
            else
            {
                Cache cache = new Cache();
                cache.dadosWrite = req.Dados.ToString();
                _distributedCache.SetString(req.Id.ToString(), cache.dadosWrite);
            }

            Response resp = new Response();
            resp.Id = req.Id;
            resp.resultado = "Informação armazenada com sucesso";
            resp.erro = false;

            return resp;
        }
    }
}
