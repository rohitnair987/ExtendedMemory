﻿using System.Threading.Tasks;
using ExtendedMemory.Models;
using Xamarin.Forms;

namespace ExtendedMemory.Helpers
{
    public interface IGetLocation
    {
        void GetUserLocation(Entry entryCity, Entry entryState, Entry entryCountry);
    }
}
