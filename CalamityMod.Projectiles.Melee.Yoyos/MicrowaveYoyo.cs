using System;
using System.IO;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee.Yoyos;

public class MicrowaveYoyo : ModProjectile
{
	public const int MaxUpdates = 3;

	private const float Radius = 100f;

	private bool spawnedAura;

	public int soundCooldown;

	public override LocalizedText DisplayName => CalamityUtils.GetItemName<TheMicrowave>();

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.YoyosLifeTimeMultiplier[base.Type] = -1f;
		ProjectileID.Sets.YoyosMaximumRange[base.Type] = TheMicrowave.Reach;
		ProjectileID.Sets.YoyosTopSpeed[base.Type] = TheMicrowave.Speed / 3f;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
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
		base.Projectile.localNPCHitCooldown = 30;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(soundCooldown);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		soundCooldown = reader.ReadInt32();
	}

	public override void AI()
	{
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		SingularSoundInstanceSystem.PlaySingleInstance(TheMicrowave.MMMSound, 6, 6, base.Projectile);
		if (base.Projectile.owner == Main.myPlayer && !spawnedAura)
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<MicrowaveAura>(), (int)((double)base.Projectile.damage * 0.35), base.Projectile.knockBack, base.Projectile.owner, base.Projectile.identity);
			spawnedAura = true;
		}
		if (soundCooldown > 0 && base.Projectile.FinalExtraUpdate())
		{
			soundCooldown--;
		}
		int numDust = 125;
		float angleIncrement = (float)Math.PI * 2f / (float)numDust;
		Vector2 dustOffset = default(Vector2);
		((Vector2)(ref dustOffset))._002Ector(100f, 0f);
		dustOffset = dustOffset.RotatedByRandom(6.2831854820251465);
		Vector2 center;
		for (int i = 0; i < numDust; i++)
		{
			Vector2 spinningpoint = dustOffset;
			double radians = angleIncrement;
			center = default(Vector2);
			dustOffset = spinningpoint.RotatedBy(radians, center);
			int dustType = Utils.SelectRandom<int>(Main.rand, ModContent.DustType<AstralOrange>(), ModContent.DustType<AstralBlue>());
			int dust = Dust.NewDust(base.Projectile.Center, 1, 1, dustType);
			Main.dust[dust].position = base.Projectile.Center + dustOffset;
			Main.dust[dust].fadeIn = 1f;
			Dust obj = Main.dust[dust];
			obj.velocity *= 0.2f;
			Main.dust[dust].scale = 0.16f;
		}
		center = base.Projectile.position - Main.player[base.Projectile.owner].position;
		if (((Vector2)(ref center)).Length() > 3200f)
		{
			base.Projectile.Kill();
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override void PostDraw(Color lightColor)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		Rectangle frame = default(Rectangle);
		((Rectangle)(ref frame))._002Ector(0, 0, 20, 16);
		Main.EntitySpriteDraw(ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/Yoyos/MicrowaveYoyoGlow", (AssetRequestMode)2).Value, base.Projectile.Center - Main.screenPosition, frame, Color.White, base.Projectile.rotation, base.Projectile.Size / 2f, 1f, (SpriteEffects)0);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 240);
		if (target.life <= 0 && soundCooldown <= 0)
		{
			SoundEngine.PlaySound(in TheMicrowave.BeepSound, base.Projectile.Center);
			soundCooldown = 45;
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 240);
		if (target.statLife <= 0 && soundCooldown <= 0)
		{
			SoundEngine.PlaySound(in TheMicrowave.BeepSound, base.Projectile.Center);
			soundCooldown = 45;
		}
	}
}
