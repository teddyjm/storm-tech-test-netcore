﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Todo.Data;
using Todo.Data.Entities;
using Todo.Models.TodoItems;

namespace Todo.Views.TodoItem
{
    public static class SelectListConvenience
    {
        public static readonly SelectListItem[] ImportanceSelectListItems =
        {
            new SelectListItem {Text = "High", Value = Importance.High.ToString()},
            new SelectListItem {Text = "Medium", Value = Importance.Medium.ToString()},
            new SelectListItem {Text = "Low", Value = Importance.Low.ToString()},
        };

        public static readonly SelectListItem[] TodoItemsSortOptionListItems =
        {
            new SelectListItem { Text = "Sort by importance", Value = TodoItemsSortOption.ByImportance.ToString() },
            new SelectListItem { Text = "Sort by rank", Value = TodoItemsSortOption.ByRank.ToString() }
        };

        public static List<SelectListItem> UserSelectListItems(this ApplicationDbContext dbContext)
        {
            return dbContext.Users.Select(u => new SelectListItem {Text = u.UserName, Value = u.Id}).ToList();
        }
    }
}