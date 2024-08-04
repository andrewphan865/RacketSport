// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Identity.Api.Models;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{ 
    [Required]
    public string CardNumber { get; set; } = "";

    [Required]
    public string SecurityNumber { get; set; } = "";

    [Required]
    [RegularExpression(@"(0[1-9]|1[0-2])\/[0-9]{2}", ErrorMessage = "Expiration should match a valid MM/YY value")]
    public string Expiration { get; set; } = "";

    [Required]
    public string CardHolderName { get; set; } = "";

    public int CardType { get; set; }

    [Required]
    public string Street { get; set; } = "";

    [Required]
    public string State { get; set; } = "";

    [Required]
    public string Country { get; set; } = "";

    [Required]
    public string PostCode { get; set; } = "";

    [Required]
    public string FirstName { get; set; } = "";

    [Required]
    public string LastName { get; set; } = "";
}
