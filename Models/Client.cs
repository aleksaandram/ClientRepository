using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace ClientRepository.Models



{
    public class Client
    {
        public int ClientRefNum { get; set; }

        [Required(ErrorMessage = "Full name is required")]
        public string FullName { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Birth date is required")]
        public DateTime BirthDate { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
    }

    public class Address
    {
        public int AddressID { get; set; }
        public int AddressType { get; set; }
        public string FullAddress { get; set; }

    }

}
