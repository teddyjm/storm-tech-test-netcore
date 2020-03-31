using System.ComponentModel.DataAnnotations;

namespace Todo.Data.Entities {
    public enum Importance
    {
        [Display(Order = 0)]
        High,
        [Display(Order = 1)]
        Medium,
        [Display(Order = 2)]
        Low,
    }
}