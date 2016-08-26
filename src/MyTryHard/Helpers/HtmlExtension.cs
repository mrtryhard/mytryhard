using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using MyTryHard.Models;
using System;
using System.Collections.Generic;

namespace MyTryHard.Helpers
{
    public static class HtmlExtension
    {
        public static IHtmlContent DropDownListForCategories(this HtmlHelper helper, string id, string name, List<Category> list)
        {
            return DropDownListForCategories(helper, id, name, list, Guid.Empty);
        }

        public static IHtmlContent DropDownListForCategories(this IHtmlHelper helper, string id, string name, List<Category> list, Guid selected)
        {        
            ViewDataDictionary vdd = new ViewDataDictionary(helper.ViewContext.ViewData);
            vdd.Add("Selected", selected);
            vdd.Add("Id", id);
            vdd.Add("Name", name);

            return helper.Partial("HtmlHelper/CategoriesList", list, vdd);
        }
    }
}
