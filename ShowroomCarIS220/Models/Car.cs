﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShowroomCarIS220.Models
{
    public class Car
    {
        [Key]
        public Guid id { get; set; }
        [Required]
        public string macar { get; set; } = "OT0";
        [Required]
        public string ten { get; set; }
        [Required]
        public string thuonghieu { get; set; }
        [Required]
        public string dongco { get; set; }
        [Required]
        public int socho { get; set; }
        [Required]
        public string kichthuoc { get; set; }
        [Required]
        public string nguongoc { get; set; }
        [Required]
        public string  vantoctoida { get; set; }
        [Required]
        public string dungtich { get; set; }
        [Required]
        public string tieuhaonhienlieu { get; set; }
        [Required]
        public string congsuatcucdai { get; set; }
        [Required]
        public string mausac { get; set; }
        [Required]
        public long gia { get; set; }
        [Required]
        public string hinhanh { get; set; }
        [Required]
        public string mota { get; set; }
        [Required]
        public int namsanxuat { get; set; }
        [Required]
        public int soluong { get; set; } = 0;
        [Required]
        public bool advice { get; set; } = false;
        [Required]
        public DateTime createdAt { get; set;}
        [Required]
        public DateTime updatedAt { get; set; }
    }
}
