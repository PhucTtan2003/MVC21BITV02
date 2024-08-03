using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SixTMidTest.Data;

public partial class NhaCungCap
{
    public string MaNcc { get; set; } = null!;

    public string TenCongTy { get; set; } = null!;

    public string Logo { get; set; } = null!;

    public string? NguoiLienLac { get; set; }
    [EmailAddress]
    public string Email { get; set; } = null!;
    [RegularExpression("0[3789][0-9]{8}")]
    public string? DienThoai { get; set; }
    [RegularExpression("[a-zA-Z 0-9]*")]
    public string? DiaChi { get; set; }
    [DataType(DataType.MultilineText)]
    public string? MoTa { get; set; }

    public virtual ICollection<HangHoa> HangHoas { get; set; } = new List<HangHoa>();
}
