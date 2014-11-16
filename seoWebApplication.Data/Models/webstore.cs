using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace seoWebApplication.Data.Models
{
    [MetadataType(typeof(webstoreMetaData))]
    public partial class webstore
    { 
       
    }

    public class webstoreMetaData
    {

       
        public System.DateTime Start_Date { get; set; }
        public int Id { get; set; }

        [Required(ErrorMessage = "Employee Number is required.")]
        public string Employee_Number { get; set; }

        [Required(ErrorMessage = "Long Name is required.")]
       
        public System.DateTime Entry_Date { get; set; }

        [Display(Name = "Leave No")]
        [System.ComponentModel.DataAnnotations.StringLength(2, ErrorMessage = "Leave No must be 2 characters or less")]
        [Required(ErrorMessage = "Leave No is required.")]
        public short Leave_No { get; set; }

        [Display(Name = "Leave Code")]
        [System.ComponentModel.DataAnnotations.StringLength(2, ErrorMessage = "Leave Code must be 2 characters or less")]
        [Required(ErrorMessage = "Leave Code is required.")]
        public short Leave_Code { get; set; }
         
        [Required(ErrorMessage = "Employee Number is required.")]
        public string webstoreName { get; set; }
        public string webstoreUrl { get; set; }
        public string locationx { get; set; }
        public string locationy { get; set; }
        public string polygonArray { get; set; }
        public string address { get; set; }

        [Display(Name = "City Code")]
        [System.ComponentModel.DataAnnotations.StringLength(1, ErrorMessage = "city Code must be 1 characters or less")]
        [Required(ErrorMessage = "City Code is required.")]
        public Nullable<int> city { get; set; }

        [Display(Name = "State Code")]
        [System.ComponentModel.DataAnnotations.StringLength(1, ErrorMessage = "State Code must be 2 characters or less")]
        [Required(ErrorMessage = "State Code is required.")]
        public Nullable<int> state { get; set; }
        [Display(Name = "Country Code")]
        [System.ComponentModel.DataAnnotations.StringLength(1, ErrorMessage = "Country Code must be 1 characters or less")]
        [Required(ErrorMessage = "Country Code is required.")]
        public Nullable<int> country { get; set; }
        public string zip { get; set; }
        [Display(Name = "Zone Code")]
        [System.ComponentModel.DataAnnotations.StringLength(1, ErrorMessage = "Zone Code must be 1 characters or less")]
        [Required(ErrorMessage = "Zone Code is required.")]
        public Nullable<int> zone { get; set; }
        [Display(Name = "Parent Webstore Id Code")]
        [System.ComponentModel.DataAnnotations.StringLength(1, ErrorMessage = "parentWebstoreId Code must be 1 characters or less")]
        [Required(ErrorMessage = "parentWebstoreId Code is required.")]
        public Nullable<int> parentWebstoreId { get; set; }
        public string email { get; set; }
        public string ownerFName { get; set; }
        public string ownerName { get; set; }
        [Display(Name = "Owner Number Code")]
        [System.ComponentModel.DataAnnotations.StringLength(1, ErrorMessage = "ownerNumber Code must be 1 characters or less")]
        [Required(ErrorMessage = "ownerNumber Code is required.")]
        public Nullable<int> ownerNumber { get; set; }
        public string managerFname { get; set; }
        public string managerLname { get; set; }
        [Display(Name = "Manager Number Code")]
        [System.ComponentModel.DataAnnotations.StringLength(1, ErrorMessage = "managerNumber Code must be 1 characters or less")]
        [Required(ErrorMessage = "managerNumber Code is required.")]
        public Nullable<int> managerNumber { get; set; }
        [Display(Name = "Store Number Code")]
        [System.ComponentModel.DataAnnotations.StringLength(1, ErrorMessage = "storeNumber Code must be 1 characters or less")]
        [Required(ErrorMessage = "storeNumber Code is required.")]
        public Nullable<int> storeNumber { get; set; }
        public string apiKey { get; set; }
        [Display(Name = "Monday Code")]
        [System.ComponentModel.DataAnnotations.StringLength(1, ErrorMessage = "monday Code must be 1 characters or less")]
        [Required(ErrorMessage = "monday Code is required.")]
        public Nullable<bool> monday { get; set; }
        public Nullable<bool> monBreakfast { get; set; }
        public Nullable<bool> monLunch { get; set; }
        public Nullable<bool> monDinner { get; set; }
        public string monBreakfastStart { get; set; }
        public string monBreakfastEnd { get; set; }
        public string monLunchStart { get; set; }
        public string monLunchEnd { get; set; }
        public string monDinnerStart { get; set; }
        public string monDinnerEnd { get; set; }
        public Nullable<bool> tuesday { get; set; }
        public Nullable<bool> tuesBreakfast { get; set; }
        public Nullable<bool> tuesLunch { get; set; }
        public Nullable<bool> tuesDinner { get; set; }
        public string tueBreakfastStart { get; set; }
        public string tueBreakfastEnd { get; set; }
        public string tueLunchStart { get; set; }
        public string tueLunchEnd { get; set; }
        public string tueDinnerStart { get; set; }
        public string tueDinnerEnd { get; set; }
        public Nullable<bool> wednesday { get; set; }
        public Nullable<bool> wedBreakfast { get; set; }
        public Nullable<bool> wedLunch { get; set; }
        public Nullable<bool> wedDinner { get; set; }
        public string wedBreakfastStart { get; set; }
        public string wedBreakfastEnd { get; set; }
        public string wedLunchStart { get; set; }
        public string wedLunchEnd { get; set; }
        public string wedDinnerStart { get; set; }
        public string wedDinnerEnd { get; set; }
        public Nullable<bool> thursday { get; set; }
        public Nullable<bool> thrBreakfast { get; set; }
        public Nullable<bool> thrLunch { get; set; }
        public Nullable<bool> thrDinner { get; set; }
        public string thrBreakfastStart { get; set; }
        public string thrBreakfastEnd { get; set; }
        public string thrLunchStart { get; set; }
        public string thrLunchEnd { get; set; }
        public string thrDinnerStart { get; set; }
        public string thrDinnerEnd { get; set; }
        public Nullable<bool> friday { get; set; }
        public Nullable<bool> friBreakfast { get; set; }
        public Nullable<bool> friLunch { get; set; }
        public Nullable<bool> friDinner { get; set; }
        public string friBreakfastStart { get; set; }
        public string friBreakfastEnd { get; set; }
        public string friLunchStart { get; set; }
        public string friLunchEnd { get; set; }
        public string friDinnerStart { get; set; }
        public string friDinnerEnd { get; set; }
        public Nullable<bool> saturday { get; set; }
        public Nullable<bool> satBreakfast { get; set; }
        public Nullable<bool> satLunch { get; set; }
        public Nullable<bool> satDinner { get; set; }
        public string satBreakfastStart { get; set; }
        public string satBreakfastEnd { get; set; }
        public string satLunchStart { get; set; }
        public string satLunchEnd { get; set; }
        public string satDinnerStart { get; set; }
        public string satDinnerEnd { get; set; }
        public Nullable<bool> sunday { get; set; }
        public Nullable<bool> sunBreakfast { get; set; }
        public Nullable<bool> sunLunch { get; set; }
        public Nullable<bool> sunDinner { get; set; }
        public string sunBreakfastStart { get; set; }
        public string sunBreakfastEnd { get; set; }
        public string sunLunchStart { get; set; }
        public string sunLunchEnd { get; set; }
        public string sunDinnerStart { get; set; }
        public string sunDinnerEnd { get; set; }
        public string thumbnail { get; set; }
        public string image { get; set; }
        public string seoDescription { get; set; }
        public string seoTitle { get; set; }
        public string seoKeywords { get; set; }
        public string seoMeta { get; set; }

    }
}
