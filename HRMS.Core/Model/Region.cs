﻿namespace HRMS.Core.Model
{
    public class Region : IntBaseEntity
    {
        public string Name { get; set; }
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }
    }

}


