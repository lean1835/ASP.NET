using System.ComponentModel.DataAnnotations;

namespace Day6_BaiTap_LeAn.Models;

public class Movie
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập tên phim.")]
    [StringLength(200)]
    [Display(Name = "Tên phim")]
    public string Title { get; set; } = string.Empty;

    [Display(Name = "Ngày phát hành")]
    [DataType(DataType.Date)]
    public DateTime ReleaseDate { get; set; }

    [Range(1, 1000, ErrorMessage = "Thời lượng phải từ 1 đến 1000 phút.")]
    [Display(Name = "Thời lượng (phút)")]
    public int Duration { get; set; }

    [Display(Name = "Thể loại")]
    public int GenreId { get; set; }

    public Genre? Genre { get; set; }
}
