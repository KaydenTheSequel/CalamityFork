using System;
using System.IO;
using System.Linq;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.NPCs;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Healing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee.Yoyos;

[PierceResistException(false)]
public class LaceratorYoyo : ModProjectile
{
	public const int MaxUpdates = 3;

	public float chargeProgress;

	private bool sawHit;

	private bool spawnedBlood;

	private int sawDir;

	public override LocalizedText DisplayName => CalamityUtils.GetItemName<Lacerator>();

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.YoyosLifeTimeMultiplier[base.Type] = -1f;
		ProjectileID.Sets.YoyosMaximumRange[base.Type] = Lacerator.Reach;
		ProjectileID.Sets.YoyosTopSpeed[base.Type] = Lacerator.Speed / 3f;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 8;
		ProjectileID.Sets.TrailingMode[base.Type] = 1;
	}

	public override void SetDefaults()
	{
		base.Projectile.aiStyle = 99;
		base.Projectile.width = (base.Projectile.height = 16);
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.MeleeNoSpeed;
		base.Projectile.penetrate = -1;
		base.Projectile.MaxUpdates = 3;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 18;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(chargeProgress);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		chargeProgress = reader.ReadSingle();
	}

	public override void AI()
	{
		//IL_0333: Unknown result type (might be due to invalid IL or missing references)
		//IL_0349: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		if (sawDir == 0)
		{
			sawDir = ((!(base.Projectile.velocity.X < 0f)) ? 1 : (-1));
		}
		if (chargeProgress > 1f)
		{
			chargeProgress = 1f;
		}
		Vector2 center;
		if (chargeProgress > 0f)
		{
			if (Main.player[base.Projectile.owner].miscCounter % 5 == 0 && base.Projectile.FinalExtraUpdate() && chargeProgress > 0.05f)
			{
				spawnedBlood = false;
				base.Projectile.position = base.Projectile.Center;
				base.Projectile.width = 196;
				base.Projectile.height = 196;
				base.Projectile.Center = base.Projectile.position;
				base.Projectile.originalDamage = base.Projectile.damage;
				base.Projectile.damage = (int)((float)base.Projectile.damage * MathHelper.Lerp(0f, 0.75f, chargeProgress));
				sawHit = true;
				base.Projectile.usesIDStaticNPCImmunity = true;
				base.Projectile.aiStyle = -1;
				base.Projectile.Damage();
				base.Projectile.aiStyle = 99;
				base.Projectile.usesIDStaticNPCImmunity = false;
				sawHit = false;
				base.Projectile.damage = base.Projectile.originalDamage;
				base.Projectile.position = base.Projectile.Center;
				base.Projectile.width = 16;
				base.Projectile.height = 16;
				base.Projectile.Center = base.Projectile.position;
			}
			if (chargeProgress > 0.05f)
			{
				float particleSize = 0.9f + 0.15f * (float)Math.Cos(Main.GlobalTimeWrappedHourly % 60f * ((float)Math.PI * 2f));
				particleSize *= 0.5f;
				Vector2 val = base.Projectile.Center + Utils.RotatedByRandom(new Vector2(0f, 84f), 6.2831854820251465);
				Vector2 spinningpoint = val.DirectionTo(base.Projectile.Center);
				double radians = (float)(-sawDir) * ((float)Math.PI / 2f) * 0.9f;
				center = default(Vector2);
				Vector2 particleVel = spinningpoint.RotatedBy(radians, center).RotatedByRandom(0.10000000149011612) * Main.rand.NextFloat(15f, 25f);
				GeneralParticleHandler.SpawnParticle(new CustomSpark(val, particleVel, "CalamityMod/Particles/PearlParticleGlow", affectedByGravity: false, (int)MathHelper.Lerp(7f, 17f, chargeProgress), particleSize, Color.DarkRed, new Vector2(0.5f, 1f), useAddativeBlend: false));
			}
			chargeProgress -= 0.003f / (float)base.Projectile.extraUpdates;
			if (chargeProgress < 0f)
			{
				chargeProgress = 0f;
			}
		}
		center = base.Projectile.position - Main.player[base.Projectile.owner].position;
		if (((Vector2)(ref center)).Length() > 3200f)
		{
			base.Projectile.Kill();
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		if (sawHit)
		{
			Vector2 bloodpos = base.Projectile.Center + base.Projectile.DirectionTo(target.Center) * 84f;
			if (!spawnedBlood && Main.rand.NextBool() && chargeProgress > 0.2f)
			{
				Rectangle hitbox = target.Hitbox;
				if (((Rectangle)(ref hitbox)).Contains(bloodpos.ToPoint()))
				{
					Projectile.NewProjectile(base.Projectile.GetSource_OnHit(target), bloodpos, base.Projectile.DirectionTo(target.Center).RotatedBy((float)sawDir * ((float)Math.PI / 2f) * 0.9f).RotatedByRandom(0.10000000149011612) * Main.rand.NextFloat(3f, 5f), ModContent.ProjectileType<BloodstoneHealOrb>(), 4, 0f, base.Projectile.owner);
					spawnedBlood = true;
				}
			}
		}
		else
		{
			Main.projectile.First((Projectile x) => x.active && x.type == ModContent.ProjectileType<LaceratorYoyo>() && x.owner == base.Projectile.owner).ModProjectile<LaceratorYoyo>().chargeProgress += (Main.player[base.Projectile.owner].yoyoGlove ? 0.05f : 0.1f);
			base.Projectile.netUpdate = true;
			target.AddBuff(ModContent.BuffType<Laceration>(), 180);
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<Laceration>(), 180);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		if (chargeProgress > 0f)
		{
			Player owner = Main.player[base.Projectile.owner];
			Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/LaceratorSaw", (AssetRequestMode)2).Value;
			Texture2D value = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/BlazingPhantomBlade", (AssetRequestMode)2).Value;
			float rot = (float)Math.PI * 60f * (float)sawDir * ((float)owner.miscCounter / 300f);
			Color color = default(Color);
			((Color)(ref color))._002Ector(200, 200, 200);
			Rectangle frame = value.Frame(1, 4);
			Main.EntitySpriteDraw(value, base.Projectile.Center - Main.screenPosition + rot.ToRotationVector2() * 30f, frame, color * MathF.Pow(chargeProgress, 0.5f) * 0.33f, rot - 0.2f * (float)sawDir, new Vector2((float)frame.Width, (float)frame.Height) * 0.5f, 1f, (SpriteEffects)((sawDir == -1) ? 2 : 0));
			Main.EntitySpriteDraw(value, base.Projectile.Center - Main.screenPosition + (rot + (float)Math.PI).ToRotationVector2() * 30f, frame, color * MathF.Pow(chargeProgress, 0.5f) * 0.33f, rot + (float)Math.PI - 0.2f * (float)sawDir, new Vector2((float)frame.Width, (float)frame.Height) * 0.5f, 1f, (SpriteEffects)((sawDir == -1) ? 2 : 0));
			((Color)(ref color))._002Ector(255, 10, 10);
			Main.EntitySpriteDraw(texture, base.Projectile.Center - Main.screenPosition, null, color * MathF.Pow(chargeProgress, 0.5f) * 1f, rot, texture.Size() * 0.5f, 1f, (SpriteEffects)(sawDir != -1));
		}
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}
}
