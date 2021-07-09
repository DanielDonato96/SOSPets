﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SOSPets.DBAcess
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class SOSPETSEntities : DbContext
    {
        public SOSPETSEntities()
            : base("name=SOSPETSEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Animai> Animais { get; set; }
        public virtual DbSet<AnimaisCategoria> AnimaisCategorias { get; set; }
        public virtual DbSet<Propaganda> Propaganda { get; set; }
        public virtual DbSet<cidade> cidade { get; set; }
        public virtual DbSet<estado> estado { get; set; }
        public virtual DbSet<SituacaoAnimal> SituacaoAnimal { get; set; }
    
        public virtual ObjectResult<vwAnimalDetail> proc_001_GetAnimalDetail(Nullable<int> animalID)
        {
            var animalIDParameter = animalID.HasValue ?
                new ObjectParameter("AnimalID", animalID) :
                new ObjectParameter("AnimalID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<vwAnimalDetail>("proc_001_GetAnimalDetail", animalIDParameter);
        }
    
        public virtual ObjectResult<vwAnimalList> proc_002_GetAnimalList(Nullable<int> estadoID, Nullable<int> cidadeID, string sort, string dir, Nullable<int> start, Nullable<int> limit, ObjectParameter totalRecord)
        {
            var estadoIDParameter = estadoID.HasValue ?
                new ObjectParameter("EstadoID", estadoID) :
                new ObjectParameter("EstadoID", typeof(int));
    
            var cidadeIDParameter = cidadeID.HasValue ?
                new ObjectParameter("CidadeID", cidadeID) :
                new ObjectParameter("CidadeID", typeof(int));
    
            var sortParameter = sort != null ?
                new ObjectParameter("Sort", sort) :
                new ObjectParameter("Sort", typeof(string));
    
            var dirParameter = dir != null ?
                new ObjectParameter("Dir", dir) :
                new ObjectParameter("Dir", typeof(string));
    
            var startParameter = start.HasValue ?
                new ObjectParameter("Start", start) :
                new ObjectParameter("Start", typeof(int));
    
            var limitParameter = limit.HasValue ?
                new ObjectParameter("Limit", limit) :
                new ObjectParameter("Limit", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<vwAnimalList>("proc_002_GetAnimalList", estadoIDParameter, cidadeIDParameter, sortParameter, dirParameter, startParameter, limitParameter, totalRecord);
        }
    
        public virtual ObjectResult<vwAnimalEstados> proc_003_ConsultaAnimaisPorEstado()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<vwAnimalEstados>("proc_003_ConsultaAnimaisPorEstado");
        }
    
        public virtual ObjectResult<vwAnimalCidades> proc_004_ConsultaAnimaisCidade(Nullable<int> estadoID)
        {
            var estadoIDParameter = estadoID.HasValue ?
                new ObjectParameter("EstadoID", estadoID) :
                new ObjectParameter("EstadoID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<vwAnimalCidades>("proc_004_ConsultaAnimaisCidade", estadoIDParameter);
        }
    }
}
