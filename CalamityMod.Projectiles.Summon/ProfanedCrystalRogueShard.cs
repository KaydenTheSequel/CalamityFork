using System;
using System.IO;
using CalamityMod.CalPlayer;
using CalamityMod.Utilities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class ProfanedCrystalRogueShard : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.SummonTagDamageMultiplier[base.Type] = 0.25f;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 34;
		base.Projectile.height = 34;
		base.Projectile.friendly = true;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = 1;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 150;
		base.Projectile.minion = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.scale = 0.8f;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	private void ai()
	{
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ba: Unknown result type (might be due to invalid IL or missing references)
		Player Owner = Main.player[base.Projectile.owner];
		base.Projectile.damage = (int)Owner.GetTotalDamage<SummonDamageClass>().ApplyTo(base.Projectile.originalDamage);
		if (base.Projectile.timeLeft > 120)
		{
			base.Projectile.rotation++;
			base.Projectile.velocity.X *= 0.985f;
			base.Projectile.velocity.Y *= 0.985f;
			return;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + MathHelper.ToRadians(135f);
		if (base.Projectile.spriteDirection == -1)
		{
			base.Projectile.rotation -= (float)Math.PI / 2f;
		}
		float num535 = base.Projectile.position.X;
		float num536 = base.Projectile.position.Y;
		float num537 = 2000f;
		bool flag19 = false;
		NPC ownerMinionAttackTargetNPC2 = base.Projectile.OwnerMinionAttackTargetNPC;
		if (ownerMinionAttackTargetNPC2 != null && ownerMinionAttackTargetNPC2.CanBeChasedBy(base.Projectile))
		{
			float num539 = ownerMinionAttackTargetNPC2.position.X + (float)(ownerMinionAttackTargetNPC2.width / 2);
			float num540 = ownerMinionAttackTargetNPC2.position.Y + (float)(ownerMinionAttackTargetNPC2.height / 2);
			float num541 = Math.Abs(base.Projectile.position.X + (float)(base.Projectile.width / 2) - num539) + Math.Abs(base.Projectile.position.Y + (float)(base.Projectile.height / 2) - num540);
			if (num541 < num537)
			{
				num537 = num541;
				num535 = num539;
				num536 = num540;
				flag19 = true;
			}
		}
		if (!flag19)
		{
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				if (Main.npc[i].CanBeChasedBy(base.Projectile))
				{
					float num543 = Main.npc[i].position.X + (float)(Main.npc[i].width / 2);
					float num544 = Main.npc[i].position.Y + (float)(Main.npc[i].height / 2);
					float num545 = Math.Abs(base.Projectile.position.X + (float)(base.Projectile.width / 2) - num543) + Math.Abs(base.Projectile.position.Y + (float)(base.Projectile.height / 2) - num544);
					if (num545 < num537)
					{
						num537 = num545;
						num535 = num543;
						num536 = num544;
						flag19 = true;
					}
				}
			}
		}
		if (flag19)
		{
			if (Main.rand.NextBool())
			{
				base.Projectile.timeLeft++;
			}
			base.Projectile.tileCollide = false;
			float num550 = 40f;
			Vector2 vector43 = base.Projectile.Center;
			float num551 = num535 - vector43.X;
			float num552 = num536 - vector43.Y;
			float num553 = (float)Math.Sqrt(num551 * num551 + num552 * num552);
			if (num553 < 100f)
			{
				num550 = 28f;
			}
			num553 = num550 / num553;
			num551 *= num553;
			num552 *= num553;
			base.Projectile.velocity.X = (base.Projectile.velocity.X * 14f + num551) / 15f;
			base.Projectile.velocity.Y = (base.Projectile.velocity.Y * 14f + num552) / 15f;
			Projectile projectile = base.Projectile;
			projectile.velocity *= 1.05f;
		}
		else
		{
			base.Projectile.tileCollide = true;
		}
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.Projectile.localAI[0]);
		writer.Write(base.Projectile.localAI[1]);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.Projectile.localAI[0] = reader.ReadSingle();
		base.Projectile.localAI[1] = reader.ReadSingle();
	}

	public override bool PreAI()
	{
		base.Projectile.Calamity().overridesMinionDamagePrevention = true;
		return true;
	}

	public override void AI()
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_032e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		//IL_037c: Unknown result type (might be due to invalid IL or missing references)
		//IL_037e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0384: Unknown result type (might be due to invalid IL or missing references)
		//IL_0389: Unknown result type (might be due to invalid IL or missing references)
		//IL_0390: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0409: Unknown result type (might be due to invalid IL or missing references)
		//IL_040e: Unknown result type (might be due to invalid IL or missing references)
		ai();
		Color newColor2 = Main.hslToRgb(base.Projectile.ai[0], 1f, 0.5f);
		if (base.Projectile.alpha > 0)
		{
			base.Projectile.alpha -= 8;
		}
		if (base.Projectile.alpha < 0)
		{
			base.Projectile.alpha = 0;
		}
		if (base.Projectile.alpha == 0)
		{
			Lighting.AddLight(base.Projectile.Center, ((Color)(ref newColor2)).ToVector3() * 0.5f);
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() - (float)Math.PI / 2f;
		for (int num979 = 0; num979 < 2; num979++)
		{
			if (Main.rand.NextBool(10))
			{
				Vector2 value55 = Vector2.UnitY.RotatedBy((float)num979 * (float)Math.PI).RotatedBy(base.Projectile.rotation);
				Dust obj = Main.dust[Dust.NewDust(base.Projectile.Center, 0, 0, 267, 0f, 0f, 225, newColor2)];
				obj.noGravity = true;
				obj.noLight = true;
				obj.scale = base.Projectile.Opacity * base.Projectile.localAI[0];
				obj.position = base.Projectile.Center;
				obj.velocity = value55 * 2.5f;
			}
		}
		for (int i = 0; i < 2; i++)
		{
			if (Main.rand.NextBool(10))
			{
				Vector2 value56 = Vector2.UnitY.RotatedBy((float)i * (float)Math.PI);
				Dust obj2 = Main.dust[Dust.NewDust(base.Projectile.Center, 0, 0, 267, 0f, 0f, 225, newColor2)];
				obj2.noGravity = true;
				obj2.noLight = true;
				obj2.scale = base.Projectile.Opacity * base.Projectile.localAI[0];
				obj2.position = base.Projectile.Center;
				obj2.velocity = value56 * 2.5f;
			}
		}
		if (Main.rand.NextBool(10))
		{
			float scaleFactor13 = 1f + Main.rand.NextFloat() * 2f;
			float fadeIn = 1f + Main.rand.NextFloat();
			float num981 = 1f + Main.rand.NextFloat();
			Vector2 vector136 = Utils.RandomVector2(Main.rand, -1f, 1f);
			if (vector136 != Vector2.Zero)
			{
				((Vector2)(ref vector136)).Normalize();
			}
			vector136 *= 20f + Main.rand.NextFloat() * 100f;
			Vector2 vector137 = base.Projectile.Center + vector136;
			Point point3 = vector137.ToTileCoordinates();
			bool flag52 = true;
			if (!WorldGen.InWorld(point3.X, point3.Y))
			{
				flag52 = false;
			}
			if (flag52 && WorldGen.SolidTile(point3.X, point3.Y))
			{
				flag52 = false;
			}
			if (flag52)
			{
				Dust obj3 = Main.dust[Dust.NewDust(vector137, 0, 0, 267, 0f, 0f, 127, newColor2)];
				obj3.noGravity = true;
				obj3.position = vector137;
				obj3.velocity = -Vector2.UnitY * scaleFactor13 * (Main.rand.NextFloat() * 0.9f + 1.6f);
				obj3.fadeIn = fadeIn;
				obj3.scale = num981;
				obj3.noLight = true;
				Dust dust = DustExtensions.BetterCloneDust(obj3);
				dust.scale *= 0.65f;
				dust.fadeIn *= 0.65f;
				dust.color = new Color(255, 255, 255, 255);
			}
		}
	}

	public override bool? CanHitNPC(NPC target)
	{
		if (base.Projectile.timeLeft > 120)
		{
			return false;
		}
		return null;
	}

	public override bool CanHitPvp(Player target)
	{
		return base.Projectile.timeLeft <= 120;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		CalamityPlayer calamityPlayer = Main.player[base.Projectile.owner].Calamity();
		bool empowered = calamityPlayer.pscState == 3;
		calamityPlayer.rollBabSpears((base.Projectile.ai[0] != 0f) ? (empowered ? 30 : 10) : 0, target.chaseable);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item27, base.Projectile.position);
		Vector2 spinningpoint = Utils.RotatedByRandom(new Vector2(0f, -3f), 3.1415927410125732);
		float num69 = Main.rand.Next(7, 13);
		Vector2 value5 = default(Vector2);
		((Vector2)(ref value5))._002Ector(2.1f, 2f);
		Color newColor = Main.hslToRgb(base.Projectile.ai[0], 1f, 0.5f);
		((Color)(ref newColor)).A = byte.MaxValue;
		for (float num70 = 0f; num70 < num69; num70++)
		{
			int num71 = Dust.NewDust(base.Projectile.Center, 0, 0, 267, 0f, 0f, 0, newColor);
			Main.dust[num71].position = base.Projectile.Center;
			Main.dust[num71].velocity = spinningpoint.RotatedBy((float)Math.PI * 2f * num70 / num69) * value5 * (0.8f + Main.rand.NextFloat() * 0.4f);
			Main.dust[num71].noGravity = true;
			Main.dust[num71].scale = 2f;
			Main.dust[num71].fadeIn = Main.rand.NextFloat() * 2f;
			Dust dust = DustExtensions.BetterCloneDust(num71);
			dust.scale /= 2f;
			dust.fadeIn /= 2f;
			dust.color = new Color(255, 255, 255, 255);
		}
		for (float num73 = 0f; num73 < num69; num73++)
		{
			int num74 = Dust.NewDust(base.Projectile.Center, 0, 0, 267, 0f, 0f, 0, newColor);
			Main.dust[num74].position = base.Projectile.Center;
			Main.dust[num74].velocity = spinningpoint.RotatedBy((float)Math.PI * 2f * num73 / num69) * value5 * (0.8f + Main.rand.NextFloat() * 0.4f);
			Dust obj = Main.dust[num74];
			obj.velocity *= Main.rand.NextFloat() * 0.8f;
			obj.noGravity = true;
			obj.scale = Main.rand.NextFloat() * 1f;
			obj.fadeIn = Main.rand.NextFloat() * 2f;
			Dust dust2 = DustExtensions.BetterCloneDust(num74);
			dust2.scale /= 2f;
			dust2.fadeIn /= 2f;
			dust2.color = new Color(255, 255, 255, 255);
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		return new Color(255 - base.Projectile.alpha, 255 - base.Projectile.alpha, 255 - base.Projectile.alpha, 0);
	}
}
