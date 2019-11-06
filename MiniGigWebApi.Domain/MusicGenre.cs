namespace MiniGigWebApi.Domain
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Class which contains the music genre data
    /// </summary>
    public class MusicGenre : BaseEntity
    {
        // Alternate keys can not be configured using Data Annotations.
        [Display(Name = "Music Genre")]
        public string Category { get; set; }
    }
}