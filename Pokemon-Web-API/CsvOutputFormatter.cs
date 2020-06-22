using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Entities.JsonModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;

namespace Pokemon_Web_API
{
    public class CsvOutputFormatter : TextOutputFormatter
    {
        public CsvOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanWriteType(Type type)
        {
            if (typeof(NewRootObject).IsAssignableFrom(type) 
                || typeof(Entities.GetPokemonModels.Pokemon).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }

            return false;
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            var buffer = new StringBuilder();

            if (context.Object is NewRootObject)
            {
                foreach (var result in (List<NResult>) context?.Object)
                {
                    FormatCsv(buffer, result);
                }
                
            }
            else
            {
                await response.WriteAsync(HttpStatusCode.NotAcceptable.ToString());
            }
            
            await response.WriteAsync(buffer.ToString());
        }
        
        private static void FormatCsv(StringBuilder buffer, NResult results)
        {
            buffer.AppendLine($"{results.Name},\"{results.Url},\"");
        }

    }
}