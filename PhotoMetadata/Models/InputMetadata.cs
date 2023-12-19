namespace PhotoMetadata.Models;

using System;
using Ganss.Excel;

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
        ArgumentNullException.ThrowIfNull(other);

        this.PhotoId = other.PhotoId;
        this.Tasks = other.Tasks;
        this.DateTaken = other.DateTaken;
        this.Keywords = other.Keywords;
        this.Caption = other.Caption;
        this.Description = other.Description;
        this.Comments = other.Comments;
        this.GpsCoordinates = other.GpsCoordinates;
    }

    public override bool Equals(object obj)
    {
        return obj is InputMetadata model &&
               this.PhotoId == model.PhotoId &&
               this.Tasks == model.Tasks &&
               this.DateTaken == model.DateTaken &&
               this.Keywords == model.Keywords &&
               this.Caption == model.Caption &&
               this.Description == model.Description &&
               this.Comments == model.Comments &&
               this.GpsCoordinates == model.GpsCoordinates;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(this.PhotoId, this.Tasks, this.DateTaken, this.Keywords, this.Caption, this.Description, this.Comments, this.GpsCoordinates);
    }

    public bool IsEmpty()
    {
        return string.IsNullOrEmpty(this.PhotoId)
            || this.DateTaken == default
            && string.IsNullOrEmpty(this.Keywords)
            && string.IsNullOrEmpty(this.Caption)
            && string.IsNullOrEmpty(this.Description)
            && string.IsNullOrEmpty(this.Comments)
            && string.IsNullOrEmpty(this.GpsCoordinates);
    }
}
