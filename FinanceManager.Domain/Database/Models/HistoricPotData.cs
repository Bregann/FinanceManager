﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Domain.Database.Models
{
    public class HistoricPotData
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public required int PotId { get; set; }

        [ForeignKey(nameof(PotId))]
        public virtual SpendingPot Pot { get; set; } = null!;

        [Required]
        public required decimal PotAmount { get; set; }

        [Required]
        public required decimal PotAmountSpent { get; set; }

        [Required]
        public required decimal PotAmountLeft { get; set; }

        [Required]
        public required int HistoricMonthlyDataId { get; set; }

        [ForeignKey(nameof(HistoricMonthlyDataId))]
        public virtual HistoricMonthlyData HistoricMonthlyData { get; set; } = null!;
    }
}
