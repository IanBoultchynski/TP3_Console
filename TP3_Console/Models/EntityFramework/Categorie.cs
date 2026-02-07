using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace TP3_Console.Models.EntityFramework;

[Table("categorie")]
public partial class Categorie
{

    private ILazyLoader _lazyLoader;
    public Categorie(ILazyLoader lazyLoader)
    {
        _lazyLoader = lazyLoader;
    }

    [Key]
    [Column("idcategorie")]
    public int Idcategorie { get; set; }

    [Column("nom")]
    [StringLength(50)]
    public string Nom { get; set; } = null!;

    [Column("description")]
    public string? Description { get; set; }

    [InverseProperty(nameof(Film.IdcategorieNavigation))]
    public virtual ICollection<Film> Films { get; set; } = new List<Film>();

    //private ICollection<Film> films;
    //[InverseProperty(nameof(Film.IdcategorieNavigation))]
    //public virtual ICollection<Film> Films
    //{
    //    get
    //    {
    //        return _lazyLoader.Load(this, ref films);
    //    }
    //    set { films = value; }
    //}
}
