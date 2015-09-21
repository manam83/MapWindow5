﻿// -------------------------------------------------------------------------------------------
// <copyright file="FeatureSetHelper.cs" company="MapWindow OSS Team - www.mapwindow.org">
//  MapWindow OSS Team - 2015
// </copyright>
// -------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using MW5.Api.Interfaces;

namespace MW5.Api.Helpers
{
    public static class FeatureSetHelper
    {
        /// <summary>
        /// Gets extents of the selected features.
        /// </summary>
        /// <returns>Null if there are no selected features.</returns>
        public static IEnvelope GetSelectedExtents(this IFeatureSet fs)
        {
            if (fs.NumSelected == 0)
            {
                return null;
            }

            var list = fs.Features.Where(f => f.Selected);

            var box = list.FirstOrDefault().Geometry.Extents;

            foreach (var ft in list)
            {
                box.Union(ft.Geometry.Extents);
            }

            return box;
        }

        /// <summary>
        /// Gets list of features of input dataset to be processed.
        /// </summary>
        public static List<IFeature> GetFeatures(this IFeatureSet fs, bool selectedOnly)
        {
            return selectedOnly ? fs.Features.Where(f => f.Selected).ToList() : fs.Features.ToList();
        }
    }
}