﻿using System;
using System.Globalization;
using System.Runtime.Serialization;
using MW5.Api.Concrete;
using MW5.Api.Enums;
using MW5.Api.Interfaces;

namespace MW5.Plugins.Model
{
    [DataContract]
    public class TmsProvider
    {
        public const int MinId = 1000;

        public static int GenerateId(string url)
        {
            int value = Math.Abs(url.GetHashCode());
            if (value < MinId)
            {
                value += MinId;
            }

            return value;
        }

        public TmsProvider()
        {
            SetDefaults();
        }

        private void SetDefaults()
        {
            MinZoom = 0;
            MaxZoom = 17;
            Name = string.Empty;
            Id = -1;
            Projection = TileProjection.SphericalMercator;
            Url = string.Empty;
            Description = string.Empty;

            Bounds = DefaultBounds;
        }

        public static IEnvelope DefaultBounds
        {
            get
            {
                var box = new Envelope();
                box.SetBounds(-180.0, 180.0, -85.05112878, 85.05112878);
                return box;
            }
        }

        [OnDeserializing]
        private void OnDeserializing(StreamingContext context)
        {
            SetDefaults();
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public TileProjection Projection { get; set; }

        [DataMember]
        public string Url { get; set; }

        [DataMember]
        public int MinZoom { get; set; }

        [DataMember]
        public int MaxZoom { get; set; }

        [DataMember]
        public string Description { get; set; }

        public IEnvelope Bounds
        {
            get { return new Envelope(MinX, MaxX, MinY, MaxY); }
            set
            {
                var bounds = value;

                MinX = bounds.MinX;
                MaxX = bounds.MaxX;
                MinY = bounds.MinY;
                MaxY = bounds.MaxY;
            }
        }

        public bool Editable { get; set; }

        [DataMember]
        public double MinX { get; set; }

        [DataMember]
        public double MaxX { get; set; }

        [DataMember]
        public double MinY { get; set; }

        [DataMember]
        public double MaxY { get; set; }

        [DataMember]
        public bool UseBounds { get; set; }
    }
}
