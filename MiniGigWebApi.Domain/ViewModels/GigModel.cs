namespace MiniGigWebApi.Domain
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class GigModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime GigDate { get; set; }

        public string Category { get; set; }
    }
}