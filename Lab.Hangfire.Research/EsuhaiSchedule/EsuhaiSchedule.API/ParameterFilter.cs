using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EsuhaiSchedule.API
{
    public class ParameterFilter : IOperationFilter
    {
        private const string Pattern = @"^.*?\.";

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
            {
                return;
            }

            foreach (var parameter in operation.Parameters.Where(x => x.In.ToString().Contains("query") && x.Name.Contains(".")))
            {
                parameter.Name = Regex.Replace(
                    parameter.Name,
                    Pattern,
                    string.Empty,
                    RegexOptions.IgnorePatternWhitespace);
            }
        }
    }
}
