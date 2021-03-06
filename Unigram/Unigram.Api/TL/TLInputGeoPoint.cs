// <auto-generated/>
using System;

namespace Telegram.Api.TL
{
	public partial class TLInputGeoPoint : TLInputGeoPointBase 
	{
		public Double Lat { get; set; }
		public Double Long { get; set; }

		public TLInputGeoPoint() { }
		public TLInputGeoPoint(TLBinaryReader from, bool cache = false)
		{
			Read(from, cache);
		}

		public override TLType TypeId { get { return TLType.InputGeoPoint; } }

		public override void Read(TLBinaryReader from, bool cache = false)
		{
			Lat = from.ReadDouble();
			Long = from.ReadDouble();
			if (cache) ReadFromCache(from);
		}

		public override void Write(TLBinaryWriter to, bool cache = false)
		{
			to.Write(0xF3B7ACC9);
			to.Write(Lat);
			to.Write(Long);
			if (cache) WriteToCache(to);
		}
	}
}