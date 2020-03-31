using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Todo.Data;
using Todo.Data.Entities;
using Todo.EntityModelMappers.TodoItems;
using Todo.EntityModelMappers.TodoLists;
using Todo.Models.TodoItems;
using Todo.Models.TodoLists;
using Todo.Services;
using Todo.Utils;

namespace Todo.Controllers
{
    [Authorize]
    public class TodoListController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IUserStore<IdentityUser> userStore;

        public TodoListController(ApplicationDbContext dbContext, IUserStore<IdentityUser> userStore)
        {
            this.dbContext = dbContext;
            this.userStore = userStore;
        }

        public IActionResult Index()
        {
            var userId = User.Id();
            var todoLists = dbContext.RelevantTodoLists(userId);
            var viewmodel = TodoListIndexViewmodelFactory.Create(todoLists);
            return View(viewmodel);
        }

        public IActionResult Detail(int todoListId, bool showDone = true, TodoItemsSortOption sortItemsBy = TodoItemsSortOption.ByImportance)
        {
            TodoList todoList = null;
            
            switch(sortItemsBy)
            {
                case TodoItemsSortOption.ByImportance:
                    todoList = dbContext.SingleTodoList(todoListId, i => i.Importance.GetDisplayOrder(), showDone);
                    break;
                case TodoItemsSortOption.ByRank:
                    todoList = dbContext.SingleTodoList(todoListId, i => i.Rank, showDone);
                    break;
                default:
                    throw new NotSupportedException($"Sorting by {sortItemsBy.ToString()} is not supported");
            }
            
            var newItemModel = TodoItemCreateFieldsFactory.Create(todoList, User.Id());
            var viewmodel = TodoListDetailViewmodelFactory.Create(todoList, showDone, sortItemsBy, newItemModel);
            return View(viewmodel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new TodoListFields());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TodoListFields fields)
        {
            if (!ModelState.IsValid) { return View(fields); }

            var currentUser = await userStore.FindByIdAsync(User.Id(), CancellationToken.None);

            var todoList = new TodoList(currentUser, fields.Title);

            await dbContext.AddAsync(todoList);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("Create", "TodoItem", new {todoList.TodoListId});
        }
    }
}