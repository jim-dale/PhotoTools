using System;
using Ganss.Excel;

namespace PhotoMetadata
{
    public class InputMetadata
    {
        [Column("File Name")]
        public string PhotoId { get; set; }
        public string Tasks { get; set; }
        [Column("Date Taken")]
        public DateTime DateTaken { get; set; }
        public string Keywords { get; set; }
        public string Caption { get; set; }
        public string Description { get; set; }
        public string Comments { get; set; }
        [Column("GPS Coordinates")]
        public string GpsCoordinates { get; set; }

        public InputMetadata() 
        {
        }

        public InputMetadata(InputMetadata other)
        {
            PhotoId = other.PhotoId;
            Tasks = other.Tasks;
            DateTaken = other.DateTaken;
            Keywords = other.Keywords;
            Caption = other.Caption;
            Description = other.Description;
            Comments = other.Comments;
            GpsCoordinates = other.GpsCoordinates;
        }

        public override bool Equals(object obj)
        {
            return obj is InputMetadata model &&
                   PhotoId == model.PhotoId &&
                   Tasks == model.Tasks &&
                   DateTaken == model.DateTaken &&
                   Keywords == model.Keywords &&
                   Caption == model.Caption &&
                   Description == model.Description &&
                   Comments == model.Comments &&
                   GpsCoordinates == model.GpsCoordinates;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(PhotoId, Tasks, DateTaken, Keywords, Caption, Description, Comments, GpsCoordinates);
        }

        public bool IsEmpty()
        {
            return string.IsNullOrEmpty(PhotoId)
                || (DateTaken == default
                && string.IsNullOrEmpty(Keywords)
                && string.IsNullOrEmpty(Caption)
                && string.IsNullOrEmpty(Description)
                && string.IsNullOrEmpty(Comments)
                && string.IsNullOrEmpty(GpsCoordinates));
        }
    }
}
