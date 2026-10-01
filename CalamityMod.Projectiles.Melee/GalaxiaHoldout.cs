using System.Collections.Generic;
using CalamityMod.DataStructures;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Particles;
using CalamityMod.Sounds;
using CalamityMod.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class GalaxiaHoldout : ModProjectile, ILocalizedModType, IModType
{
	private Item associatedItem;

	public new string LocalizationCategory => "Projectiles.Melee";

	private Player Owner => Main.player[base.Projectile.owner];

	public bool OwnerCanUseItem
	{
		get
		{
			if (Owner.HeldItem != associatedItem)
			{
				return false;
			}
			return (Owner.HeldItem.ModItem as FourSeasonsGalaxia).CanUseItem(Owner);
		}
	}

	public ref float Initialized => ref base.Projectile.ai[0];

	public ref float CycleDirection => ref base.Projectile.ai[1];

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 2);
		base.Projectile.aiStyle = -1;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 40;
		base.Projectile.tileCollide = false;
		base.Projectile.damage = 0;
	}

	public override void AI()
	{
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		if (Initialized != 0f)
		{
			return;
		}
		if (Owner.HeldItem.type != ModContent.ItemType<FourSeasonsGalaxia>())
		{
			base.Projectile.Kill();
			return;
		}
		base.Projectile.Center = Owner.Center;
		associatedItem = Owner.HeldItem;
		Reattune((FourSeasonsGalaxia)associatedItem.ModItem);
		Color particleColor = (associatedItem.ModItem as FourSeasonsGalaxia).mainAttunement.tooltipColor;
		for (int i = 0; i <= 5; i++)
		{
			Vector2 displace = Vector2.UnitX * 20f * Main.rand.NextFloat(-1f, 1f);
			GeneralParticleHandler.SpawnParticle(new GenericBloom(Owner.Bottom + displace, -Vector2.UnitY * Main.rand.NextFloat(1f, 5f), particleColor, 0.02f + Main.rand.NextFloat(0f, 0.2f), 20 + Main.rand.Next(30)));
		}
		for (int j = 0; j <= 10; j++)
		{
			Vector2 displace2 = Vector2.UnitX * 16f * Main.rand.NextFloat(-1f, 1f);
			GeneralParticleHandler.SpawnParticle(new GenericSparkle(Owner.Bottom + displace2, -Vector2.UnitY * Main.rand.NextFloat(1f, 5f), particleColor, particleColor, 0.5f + Main.rand.NextFloat(-0.2f, 0.2f), 20 + Main.rand.Next(30), 1f, 2f));
		}
		Initialized = 1f;
	}

	public void Reattune(FourSeasonsGalaxia item)
	{
		//IL_040f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0414: Unknown result type (might be due to invalid IL or missing references)
		//IL_0425: Unknown result type (might be due to invalid IL or missing references)
		//IL_042a: Unknown result type (might be due to invalid IL or missing references)
		//IL_043b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0440: Unknown result type (might be due to invalid IL or missing references)
		//IL_0451: Unknown result type (might be due to invalid IL or missing references)
		//IL_0456: Unknown result type (might be due to invalid IL or missing references)
		//IL_0467: Unknown result type (might be due to invalid IL or missing references)
		//IL_046c: Unknown result type (might be due to invalid IL or missing references)
		//IL_047d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0482: Unknown result type (might be due to invalid IL or missing references)
		//IL_0493: Unknown result type (might be due to invalid IL or missing references)
		//IL_0498: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04db: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0504: Unknown result type (might be due to invalid IL or missing references)
		//IL_0509: Unknown result type (might be due to invalid IL or missing references)
		//IL_0521: Unknown result type (might be due to invalid IL or missing references)
		//IL_0526: Unknown result type (might be due to invalid IL or missing references)
		//IL_0537: Unknown result type (might be due to invalid IL or missing references)
		//IL_053c: Unknown result type (might be due to invalid IL or missing references)
		//IL_054a: Unknown result type (might be due to invalid IL or missing references)
		//IL_054f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_033f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0344: Unknown result type (might be due to invalid IL or missing references)
		//IL_0356: Unknown result type (might be due to invalid IL or missing references)
		//IL_035b: Unknown result type (might be due to invalid IL or missing references)
		//IL_036d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0372: Unknown result type (might be due to invalid IL or missing references)
		//IL_0384: Unknown result type (might be due to invalid IL or missing references)
		//IL_0389: Unknown result type (might be due to invalid IL or missing references)
		//IL_039b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_070b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0716: Unknown result type (might be due to invalid IL or missing references)
		//IL_056e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0576: Unknown result type (might be due to invalid IL or missing references)
		//IL_057b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0580: Unknown result type (might be due to invalid IL or missing references)
		//IL_0585: Unknown result type (might be due to invalid IL or missing references)
		//IL_058a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0669: Unknown result type (might be due to invalid IL or missing references)
		//IL_067d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0682: Unknown result type (might be due to invalid IL or missing references)
		//IL_0696: Unknown result type (might be due to invalid IL or missing references)
		//IL_06aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_06af: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0603: Unknown result type (might be due to invalid IL or missing references)
		//IL_060d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0612: Unknown result type (might be due to invalid IL or missing references)
		//IL_061a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0624: Unknown result type (might be due to invalid IL or missing references)
		//IL_0629: Unknown result type (might be due to invalid IL or missing references)
		//IL_0633: Unknown result type (might be due to invalid IL or missing references)
		List<int> IgnoredLines = new List<int>();
		Attunement attunement = ((CycleDirection != -1f) ? (item.mainAttunement.id switch
		{
			AttunementID.Phoenix => AttunementSystem.FindOrNull(AttunementID.Andromeda), 
			AttunementID.Andromeda => AttunementSystem.FindOrNull(AttunementID.Polaris), 
			AttunementID.Polaris => AttunementSystem.FindOrNull(AttunementID.Aries), 
			_ => AttunementSystem.FindOrNull(AttunementID.Phoenix), 
		}) : (item.mainAttunement.id switch
		{
			AttunementID.Phoenix => AttunementSystem.FindOrNull(AttunementID.Aries), 
			AttunementID.Aries => AttunementSystem.FindOrNull(AttunementID.Polaris), 
			AttunementID.Polaris => AttunementSystem.FindOrNull(AttunementID.Andromeda), 
			_ => AttunementSystem.FindOrNull(AttunementID.Phoenix), 
		}));
		Vector2[] StarPositions;
		Vector2[] ExtraLines;
		Color StarColor;
		switch (attunement.id)
		{
		case AttunementID.Aries:
			StarPositions = (Vector2[])(object)new Vector2[5]
			{
				new Vector2(-160f, -150f),
				new Vector2(45f, -170f),
				new Vector2(137f, 40f),
				new Vector2(146f, 126f),
				new Vector2(129f, 151f)
			};
			ExtraLines = (Vector2[])(object)new Vector2[0];
			StarColor = Color.Orchid;
			break;
		case AttunementID.Polaris:
			StarPositions = (Vector2[])(object)new Vector2[7]
			{
				new Vector2(69f, -188f),
				new Vector2(18f, -122f),
				new Vector2(-23f, -39f),
				new Vector2(-13f, 63f),
				new Vector2(42f, 147f),
				new Vector2(-8f, 184f),
				new Vector2(-61f, 83f)
			};
			ExtraLines = (Vector2[])(object)new Vector2[1]
			{
				new Vector2(3f, 6f)
			};
			StarColor = Color.CornflowerBlue;
			break;
		case AttunementID.Andromeda:
			StarPositions = (Vector2[])(object)new Vector2[16]
			{
				new Vector2(-210f, -46f),
				new Vector2(-150f, -35f),
				new Vector2(-69f, 18f),
				new Vector2(33f, 61f),
				new Vector2(127f, 72f),
				new Vector2(-41f, -27f),
				new Vector2(-41f, -67f),
				new Vector2(-100f, -124f),
				new Vector2(-160f, -130f),
				new Vector2(15f, 147f),
				new Vector2(35f, 126f),
				new Vector2(37f, 23f),
				new Vector2(67f, -47f),
				new Vector2(126f, -109f),
				new Vector2(146f, -136f),
				new Vector2(95f, -117f)
			};
			ExtraLines = (Vector2[])(object)new Vector2[2]
			{
				new Vector2(2f, 5f),
				new Vector2(13f, 15f)
			};
			IgnoredLines.Add(5);
			IgnoredLines.Add(9);
			IgnoredLines.Add(15);
			StarColor = Color.MediumSlateBlue;
			break;
		default:
			StarPositions = (Vector2[])(object)new Vector2[12]
			{
				new Vector2(-206f, -99f),
				new Vector2(-150f, -43f),
				new Vector2(-120f, -146f),
				new Vector2(-60f, -71f),
				new Vector2(-106f, 71f),
				new Vector2(-59f, 138f),
				new Vector2(116f, -22f),
				new Vector2(246f, -26f),
				new Vector2(192f, 36f),
				new Vector2(138f, 81f),
				new Vector2(88f, -107f),
				new Vector2(84f, -75f)
			};
			ExtraLines = (Vector2[])(object)new Vector2[2]
			{
				new Vector2(3f, 10f),
				new Vector2(11f, 6f)
			};
			IgnoredLines.Add(10);
			StarColor = Color.OrangeRed;
			break;
		}
		if (GeneralParticleHandler.FreeSpacesAvailable() > StarPositions.Length * 2)
		{
			for (int i = 0; i < StarPositions.Length; i++)
			{
				GeneralParticleHandler.SpawnParticle(new GenericSparkle(Owner.Center + StarPositions[i], Vector2.Zero, Color.White, StarColor, (attunement.id == AttunementID.Polaris && i == 0) ? 3f : (Main.rand.NextFloat(1f, 1.5f) * ((attunement.id == AttunementID.Andromeda) ? 0.8f : 1f)), 20, 0f, 3f));
				if (i > 0 && !IgnoredLines.Contains(i))
				{
					GeneralParticleHandler.SpawnParticle(new BloomLineVFX(Owner.Center + StarPositions[i - 1], StarPositions[i] - StarPositions[i - 1], 0.5f, StarColor, 20, capped: true));
				}
			}
			for (int j = 0; j < ExtraLines.Length; j++)
			{
				GeneralParticleHandler.SpawnParticle(new BloomLineVFX(Owner.Center + StarPositions[(int)ExtraLines[j].Y], StarPositions[(int)ExtraLines[j].X] - StarPositions[(int)ExtraLines[j].Y], 0.5f, StarColor, 20, capped: true));
			}
		}
		SoundEngine.PlaySound(CommonCalamitySounds.LightningSound with
		{
			Volume = CommonCalamitySounds.LightningSound.Volume * 0.4f
		}, base.Projectile.Center);
		Main.LocalPlayer.SetScreenshake(5f);
		item.mainAttunement = attunement;
	}
}
