﻿/*
MVC Blanket Library Copyright (C) 2009-2012 Leonid Gordo

This library is free software; you can redistribute it and/or modify it under the terms of the GNU Lesser General Public License as published by the Free Software Foundation; 
either version 3.0 of the License, or (at your option) any later version.

This library is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. 
See the GNU Lesser General Public License for more details.

You should have received a copy of the GNU Lesser General Public License along with this library; 
if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA 
*/

using System.Web.Mvc;
using MvcBlanket;
using MvcBlanket.ViewModels;

namespace MvcBlanketLib.ViewModels
{
    public static class PagedViewModelFactory
    {
        public static PagedViewModel<T> Create<T>(ViewDataDictionary viewData, string defaultSortColumn = "", SortDirection defaultSortDirection = SortDirection.Ascending)
        {
            return new PagedViewModel<T>
                   {
                       ViewData = viewData,
                       GridSortOptions = viewData["GridSortOptions"] as GridSortOptions,
                       DefaultSortColumn = defaultSortColumn,
                       DefaultSortDirection = defaultSortDirection,
                       Page = (int) viewData["PageNumber"],
                       PageSize = (int) viewData["PageSize"]
                   };
        }

        public static PagedViewModel<T> Create<T>(ControllerContext context, string defaultSortColumn = "", SortDirection defaultSortDirection = SortDirection.Ascending)
        {
            return new PagedViewModel<T>
            {
                ControllerContext = context,
                GridSortOptions = context.Controller.ViewBag.GridSortOptions,
                DefaultSortColumn = defaultSortColumn,
                DefaultSortDirection = defaultSortDirection,
                Page = context.Controller.ViewBag.PageNumber,
                PageSize = context.Controller.ViewBag.PageSize
            };
        }
    }
}
