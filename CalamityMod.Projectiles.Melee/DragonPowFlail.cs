using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Weapons.Melee;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class DragonPowFlail : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public override void SetDefaults()
	{
		base.Projectile.width = 50;
		base.Projectile.height = 50;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.MeleeNoSpeed;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.extraUpdates = 2;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.alpha = 255;
	}

	public override void AI()
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_033a: Unknown result type (might be due to invalid IL or missing references)
		//IL_033f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0344: Unknown result type (might be due to invalid IL or missing references)
		//IL_0378: Unknown result type (might be due to invalid IL or missing references)
		//IL_037d: Unknown result type (might be due to invalid IL or missing references)
		base.DrawOffsetX = 0;
		base.DrawOriginOffsetY = -11;
		base.DrawOriginOffsetX = 0f;
		Player owner = Main.player[base.Projectile.owner];
		Vector2 posDiff = owner.Center - base.Projectile.Center;
		base.Projectile.rotation = posDiff.ToRotation();
		if (owner.dead)
		{
			base.Projectile.Kill();
			return;
		}
		if (posDiff.X < 0f)
		{
			owner.ChangeDir(1);
			base.Projectile.direction = 1;
			base.Projectile.spriteDirection = 1;
			base.Projectile.rotation += (float)Math.PI;
		}
		else
		{
			owner.ChangeDir(-1);
			base.Projectile.direction = -1;
			base.Projectile.spriteDirection = -1;
		}
		owner.itemRotation = (-1f * posDiff * (float)base.Projectile.direction).ToRotation();
		owner.itemAnimation = 6;
		owner.itemTime = 6;
		float dist = ((Vector2)(ref posDiff)).Length();
		if (base.Projectile.ai[0] == 0f && dist > 800f)
		{
			base.Projectile.ai[0] = 1f;
		}
		if (base.Projectile.ai[0] != 0f)
		{
			if (dist > 1500f)
			{
				base.Projectile.Kill();
				return;
			}
			base.Projectile.velocity = posDiff.SafeNormalize(Vector2.Zero) * DragonPow.ReturnSpeed;
			if (((Vector2)(ref posDiff)).Length() < DragonPow.ReturnSpeed)
			{
				base.Projectile.Kill();
				return;
			}
		}
		int numDust = 5;
		for (int i = 0; i < numDust; i++)
		{
			int dustType = (Main.rand.NextBool(3) ? 246 : 244);
			float scale = 0.8f + Main.rand.NextFloat(0.6f);
			int idx = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, dustType);
			Main.dust[idx].noGravity = true;
			Main.dust[idx].scale = scale;
			Dust obj = Main.dust[idx];
			obj.velocity *= 2f;
			Dust obj2 = Main.dust[idx];
			obj2.velocity += base.Projectile.velocity * 0.3f;
		}
		base.Projectile.ai[1]++;
		base.Projectile.alpha = ((!(base.Projectile.ai[1] > 5f)) ? 255 : 0);
		if (base.Projectile.ai[1] % 4f == 0f)
		{
			int type = ModContent.ProjectileType<DraconicSpark>();
			int damage = base.Projectile.damage / 8;
			float kb = 3f;
			Vector2 vel = Utils.RotatedByRandom(new Vector2(DragonPow.SparkSpeed, 0f), 6.2831854820251465);
			vel += base.Projectile.velocity * 0.05f;
			float sparkVariety = Main.rand.Next(3);
			if (base.Projectile.owner == Main.myPlayer)
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, vel, type, damage, kb, base.Projectile.owner, sparkVariety);
			}
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.player[base.Projectile.owner].WithinRange(target.Center, 345f))
		{
			target.AddBuff(ModContent.BuffType<Dragonfire>(), 180);
			base.Projectile.ai[0] = 1f;
			base.Projectile.netUpdate = true;
			PetalStorm(target.Center);
			Waterfalls(target.Center);
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		if (!base.Projectile.WithinRange(Main.player[base.Projectile.owner].Center, 345f))
		{
			target.AddBuff(ModContent.BuffType<HolyFlames>(), 180);
			base.Projectile.ai[0] = 1f;
			base.Projectile.netUpdate = true;
			PetalStorm(target.Center);
			Waterfalls(target.Center);
		}
	}

	private void PetalStorm(Vector2 targetPos)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item105, targetPos);
		int type = 221;
		int numPetals = 12;
		IEntitySource source = base.Projectile.GetSource_FromThis();
		int petalDamage = base.Projectile.damage / 8;
		float petalKB = 0f;
		for (int i = 0; i < numPetals; i++)
		{
			if (base.Projectile.owner == Main.myPlayer)
			{
				float angle = Main.rand.NextFloat((float)Math.PI * 2f);
				Projectile petal = CalamityUtils.ProjectileBarrage(source, base.Projectile.Center, targetPos, Main.rand.NextBool(), 1000f, 1400f, 80f, 900f, Main.rand.NextFloat(DragonPow.MinPetalSpeed, DragonPow.MaxPetalSpeed), type, petalDamage, petalKB, base.Projectile.owner);
				if (petal.whoAmI.WithinBounds(Main.maxProjectiles))
				{
					petal.DamageType = DamageClass.Melee;
					petal.rotation = angle;
					petal.usesLocalNPCImmunity = true;
					petal.localNPCHitCooldown = -1;
				}
			}
		}
	}

	private void Waterfalls(Vector2 targetPos)
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		int type = ModContent.ProjectileType<Waterfall>();
		int numWaterfalls = 12;
		int waterfallDamage = base.Projectile.damage / 6;
		float waterfallKB = 0f;
		Vector2 startPos = default(Vector2);
		Vector2 fallVec = default(Vector2);
		for (int i = 0; i < numWaterfalls; i++)
		{
			float startOffsetX = Main.rand.NextFloat(-120f, 120f);
			float startOffsetY = Main.rand.NextFloat(-740f, -700f);
			((Vector2)(ref startPos))._002Ector(targetPos.X + startOffsetX, targetPos.Y + startOffsetY);
			float fallSpeed = Main.rand.NextFloat(DragonPow.MinWaterfallSpeed, DragonPow.MaxWaterfallSpeed);
			((Vector2)(ref fallVec))._002Ector(0f, fallSpeed);
			fallVec = fallVec.RotatedBy(Main.rand.NextFloat(-0.08f, 0.08f));
			if (base.Projectile.owner == Main.myPlayer)
			{
				int idx = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), startPos, fallVec, type, waterfallDamage, waterfallKB, base.Projectile.owner);
				Main.projectile[idx].timeLeft = 100;
				Main.projectile[idx].tileCollide = false;
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		Vector2 mountedCenter = Main.player[base.Projectile.owner].MountedCenter;
		_ = Color.Transparent;
		Texture2D chainTex = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/DragonPowChain", (AssetRequestMode)2).Value;
		Vector2 chainDrawPos = base.Projectile.Center;
		Rectangle? sourceRectangle = null;
		Vector2 origin = default(Vector2);
		((Vector2)(ref origin))._002Ector((float)chainTex.Width * 0.5f, (float)chainTex.Height * 0.5f);
		Vector2 posDiff = mountedCenter - chainDrawPos;
		float rot = (float)Math.Atan2(posDiff.Y, posDiff.X) - (float)Math.PI / 2f;
		if (posDiff.X < 0f)
		{
			rot += (float)Math.PI;
		}
		bool keepDrawing = true;
		if (chainDrawPos.HasNaNs() || posDiff.HasNaNs())
		{
			keepDrawing = false;
		}
		while (keepDrawing && !(((Vector2)(ref posDiff)).Length() < (float)chainTex.Height + 1f))
		{
			Vector2 chainDirection = posDiff.SafeNormalize(Vector2.Zero);
			chainDrawPos += chainDirection * (float)chainTex.Height;
			posDiff = mountedCenter - chainDrawPos;
			Color colorAtLoc = Lighting.GetColor((int)chainDrawPos.X / 16, (int)chainDrawPos.Y / 16);
			Main.spriteBatch.Draw(chainTex, chainDrawPos - Main.screenPosition, sourceRectangle, colorAtLoc, rot, origin, 1f, (SpriteEffects)0, 0f);
		}
		return true;
	}
}
