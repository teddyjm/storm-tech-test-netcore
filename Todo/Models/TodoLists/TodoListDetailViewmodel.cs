using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Todo.Models.TodoItems;

namespace Todo.Models.TodoLists
{
    public class TodoListDetailViewmodel
    {
        public int TodoListId { get; }
        public string Title { get; }
        public ICollection<TodoItemSummaryViewmodel> Items { get; }
        [Display(Name = "Show done")]
        public bool ShowDone { get; set; }

        [Display(Name = "Sort by")]
        public TodoItemsSortOption SortItemsBy { get; set; }

        public TodoListDetailViewmodel(int todoListId, string title, ICollection<TodoItemSummaryViewmodel> items, bool showDone, TodoItemsSortOption sortItemsBy)
        {
            Items = items;
            TodoListId = todoListId;
            Title = title;
            ShowDone = showDone;
            SortItemsBy = sortItemsBy;
        }
    }
}