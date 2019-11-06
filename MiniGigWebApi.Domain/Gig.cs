namespace MiniGigWebApi.Domain
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Gig : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Gig Date")]
        public DateTime GigDate { get; set; } = DateTime.MinValue;

        [Display(Name = "Music Genre")]
        public int MusicGenreId { get; set; }

        public virtual MusicGenre MusicGenre { get; set; }
    }
}