﻿using System.Net.Http;
using System.Threading.Tasks;
using JsonUtils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ServiceComposer.AspNetCore;

namespace Catalog.ViewModelComposition
{
    class ProductDetailsGetHandler : ICompositionRequestsHandler
    {
        private readonly HttpClient _httpClient;

        public ProductDetailsGetHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet("/products/details/{id}")]
        public async Task Handle(HttpRequest request)
        {
            var id = (string)request.HttpContext.GetRouteData().Values["id"];

            var url = $"/api/product-details/product/{id}";
            var response = await _httpClient.GetAsync(url);

            dynamic details = await response.Content.AsExpando();

            var vm = request.GetComposedResponseModel();
            vm.ProductName = details.Name;
            vm.ProductDescription = details.Description;
        }
    }
}
