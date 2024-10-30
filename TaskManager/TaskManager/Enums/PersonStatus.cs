using System.ComponentModel.DataAnnotations;

namespace TaskManager.Api.Enums
{
    public enum PersonStatus
    {
        [Display(Name = "Disponível")]
        Available,

        [Display(Name = "Indisponível")]
        Unavailable
    }
}