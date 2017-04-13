using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TYLGallery.Common;

namespace TylData.Models
{
    public class UserVisitedImage
    {

        public int Id { get; set; }

        [Index("IX_UserImage", 1, IsUnique = true)]
        public int UserId { get; set; }
        [Index("IX_UserImage", 2, IsUnique = true)]
        public int ImageId { get; set; }

        public DateTime? VisitedOn { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Feedback Feedback { get; set; }

        public virtual Image Image { get; set; }
        public virtual User User { get; set; }
    }
}