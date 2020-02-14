namespace InfraManager.WebApi.BLL.Calls
{
    using System.ComponentModel.DataAnnotations;

    public enum CallReceiptType : byte
    {
        [Display(Name = "Телефон")]
        Phone = 0,

        [Display(Name = "E-mail")]
        Email = 1,

        [Display(Name = "Web-интерфейс")]
        Web = 2,

        [Display(Name = "Почта")]
        Mail = 3,

        [Display(Name = "Система")]
        System = 4,

        [Display(Name = "Процесс")]
        Process = 5,

        [Display(Name = "Служебная записка")]
        ServiceNote = 6
    }
}
