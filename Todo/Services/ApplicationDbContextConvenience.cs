﻿using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Todo.Data;
using Todo.Data.Entities;

namespace Todo.Services
{
    public static class ApplicationDbContextConvenience
    {
        public static IQueryable<TodoList> RelevantTodoLists(this ApplicationDbContext dbContext, string userId)
        {
            return dbContext.TodoLists.Include(tl => tl.Owner)
                .Include(tl => tl.Items)
                .Where(tl => tl.Owner.Id == userId || tl.Items.Any(item => item.ResponsiblePartyId == userId));
        }

        public static TodoList SingleTodoList<SortKeyType>(this ApplicationDbContext dbContext, int todoListId, Expression<Func<TodoItem, SortKeyType>> sortItemsBy, bool includeDoneItems)
        {
            var todoList = dbContext.TodoLists.Include(tl => tl.Owner)
                .Include(tl => tl.Items)
                .ThenInclude(ti => ti.ResponsibleParty)
                .Single(tl => tl.TodoListId == todoListId);

            todoList.Items = todoList.Items.AsQueryable().Where(item => includeDoneItems || !item.IsDone).OrderBy(sortItemsBy).ToList();

            return todoList;
        }

        public static TodoItem SingleTodoItem(this ApplicationDbContext dbContext, int todoItemId)
        {
            return dbContext.TodoItems.Include(ti => ti.TodoList).Single(ti => ti.TodoItemId == todoItemId);
        }
    }
}