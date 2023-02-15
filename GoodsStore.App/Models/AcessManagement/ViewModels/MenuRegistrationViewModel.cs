using GoodsStore.App.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;

namespace GoodsStore.App.Models.AccessManagement.ViewModels
{
    public class MenuRegistrationViewModel
    {
        #region Properties / fields
        [Key]
        public int MenuId { get; set; }
        
        [Required(ErrorMessage = "Main Menu is required")]
        public string MainMenuId { get; set; }

        public MainMenu? MainMenu { get; set; }

        [AllowNull]
        public SelectList? MainMenus { get; set; }

        [AllowNull]
        public DateTime DtRegistered { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Label name is required")]
        public string Label { get; set; }

        [AllowNull]
        public int? Sequence { get; set; }

        [AllowNull]
        public string? Action { get; set; }
                
        [Required(ErrorMessage = "Status is required")]
        public string Status { get; set; }
        #endregion

        #region Public Methods / Builder
        public MenuRegistrationViewModel FillMainMenus(SelectList mainMenus)
        {
            this.MainMenus = mainMenus;
            return this;
        }
        #endregion
    }
}
