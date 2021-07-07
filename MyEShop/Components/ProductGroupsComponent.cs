using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyEShop.Context;
using MyEShop.Data.Reposiroties;
using MyEShop.Models;

namespace MyEShop.Components
{
    public class ProductGroupsComponent : ViewComponent
    {
        private IGroupRepository _groupRepository;

        public ProductGroupsComponent(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public IViewComponentResult Invoke()
        {
            var categories = _groupRepository.GetGroupForShow();
            return View("/Views/Components/ProductGroupsComponent.cshtml", categories);
        }

    }
}
