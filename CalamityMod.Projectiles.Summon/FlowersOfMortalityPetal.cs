using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class FlowersOfMortalityPetal : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public Player Owner => Main.player[base.Projectile.owner];

	public ref float OffsetAngle => ref base.Projectile.ai[0];

	public ref float Time => ref base.Projectile.ai[1];

	public float Hue => OffsetAngle % ((float)Math.PI * 2f) / ((float)Math.PI * 2f) % 1f;

	public override void SetStaticDefaults()
	{
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 8;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 30);
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.minionSlots = 0.6f;
		base.Projectile.timeLeft = 90000;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.minion = true;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		bool num = base.Projectile.type == ModContent.ProjectileType<FlowersOfMortalityPetal>();
		Player player = Main.player[base.Projectile.owner];
		CalamityPlayer modPlayer = player.Calamity();
		player.AddBuff(ModContent.BuffType<FlowersOfMortalityBuff>(), 3600);
		if (num)
		{
			if (player.dead)
			{
				modPlayer.flowersOfMortality = false;
			}
			if (modPlayer.flowersOfMortality)
			{
				base.Projectile.timeLeft = 2;
			}
		}
		SetProjectileDamage();
		Time++;
		NPC potentialTarget = base.Projectile.Center.MinionHoming(1050f, Owner);
		if (Time % 50f == 49f && Main.myPlayer == base.Projectile.owner && potentialTarget != null)
		{
			Vector2 shootVelocity = base.Projectile.SafeDirectionTo(potentialTarget.Center) * 10f;
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, shootVelocity, ModContent.ProjectileType<MortalityBeam>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
		}
		base.Projectile.Center = player.Center + OffsetAngle.ToRotationVector2() * (150f + (float)Math.Sin(Time * 0.08f) * 15f) + Vector2.UnitY * Owner.gfxOffY;
		base.Projectile.rotation += MathHelper.ToRadians(7f);
		OffsetAngle += MathHelper.ToRadians(6f);
	}

	public void SetProjectileDamage()
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.localAI[0] == 0f)
		{
			for (int i = 0; i < 36; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 261);
				dust.noGravity = true;
				dust.color = Main.hslToRgb(Main.rand.NextFloat(), 1f, 0.5f);
				dust.velocity = Main.rand.NextVector2Unit() * Main.rand.NextFloat(2f, 7f);
			}
			base.Projectile.localAI[0]++;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		Texture2D petalTexture = TextureAssets.Projectile[base.Type].Value;
		Texture2D coreTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/FlowersOfMortalityCore", (AssetRequestMode)2).Value;
		Color drawColor = Main.hslToRgb(Hue, 0.95f, 0.5f) * 2.3f;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		Main.EntitySpriteDraw(petalTexture, drawPosition, null, base.Projectile.GetAlpha(drawColor), base.Projectile.rotation, petalTexture.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0);
		Main.EntitySpriteDraw(coreTexture, drawPosition, null, base.Projectile.GetAlpha(lightColor), 0f, coreTexture.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<ElementalMix>(), 30);
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
