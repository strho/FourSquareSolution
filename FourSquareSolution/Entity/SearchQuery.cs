using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FourSquareSolution.Entity
{
    /// <summary>
    ///     SearchQuery entity represents query for venues used by user
    /// </summary>
    public class SearchQuery
    {
        /// <summary>
        ///     Id of query
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <summary>
        ///     String representation of query
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        ///     How many times query was used
        /// </summary>
        public int Count { get; set; }
    }
}