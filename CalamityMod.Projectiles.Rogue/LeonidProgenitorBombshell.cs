using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class LeonidProgenitorBombshell : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/LeonidProgenitor";

	public override void SetDefaults()
	{
		base.Projectile.width = 32;
		base.Projectile.height = 32;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 180;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void AI()
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		int randomDust = Utils.SelectRandom<int>(Main.rand, ModContent.DustType<AstralOrange>(), ModContent.DustType<AstralBlue>());
		int astral = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, randomDust, 0f, 0f, 100, CalamityUtils.ColorSwap(LeonidProgenitor.blueColor, LeonidProgenitor.purpleColor, 1f), 0.8f);
		Main.dust[astral].noGravity = true;
		Dust obj = Main.dust[astral];
		obj.velocity *= 0f;
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] > 10f)
		{
			base.Projectile.ai[0] = 10f;
			if (base.Projectile.velocity.Y == 0f && base.Projectile.velocity.X != 0f)
			{
				base.Projectile.velocity.X = base.Projectile.velocity.X * 0.97f;
				if (base.Projectile.velocity.X > -0.01f && base.Projectile.velocity.X < 0.01f)
				{
					base.Projectile.velocity.X = 0f;
					base.Projectile.netUpdate = true;
				}
			}
			base.Projectile.velocity.Y += 0.2f / (float)base.Projectile.MaxUpdates;
		}
		base.Projectile.rotation += base.Projectile.velocity.X * 0.1f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		Main.EntitySpriteDraw(tex, base.Projectile.Center - Main.screenPosition, null, lightColor, base.Projectile.rotation, tex.Size() / 2f, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}

	public override void PostDraw(Color lightColor)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Rogue/LeonidProgenitorGlow", (AssetRequestMode)2).Value;
		Main.EntitySpriteDraw(tex, base.Projectile.Center - Main.screenPosition, null, Color.White, base.Projectile.rotation, tex.Size() / 2f, base.Projectile.scale, (SpriteEffects)0);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item62, base.Projectile.position);
		if (Main.myPlayer == base.Projectile.owner)
		{
			int flash = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<Flash>(), base.Projectile.damage, 0f, base.Projectile.owner, 0f, 1f);
			if (flash.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[flash].DamageType = RogueDamageClass.Instance;
			}
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 240);
		SpawnExtraProjectiles(target);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 240);
		SpawnExtraProjectiles(target);
	}

	private void SpawnExtraProjectiles(Entity target)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		Vector2 pos = default(Vector2);
		((Vector2)(ref pos))._002Ector(target.Center.X + (float)Main.rand.Next(-201, 201), Main.screenPosition.Y - 600f - (float)Main.rand.Next(50));
		Vector2 meteorVel = CalamityUtils.CalculatePredictiveAimToTargetMaxUpdates(pos, target, 20f, 3);
		Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), pos, meteorVel, ModContent.ProjectileType<LeonidCometBig>(), base.Projectile.damage / 2, base.Projectile.knockBack, base.Projectile.owner, 0f, 0.5f + Main.rand.NextFloat() * 0.3f);
		if (base.Projectile.Calamity().stealthStrike)
		{
			Vector2 cometPos = default(Vector2);
			for (int i = 0; i < 5; i++)
			{
				((Vector2)(ref cometPos))._002Ector(target.Center.X + (float)Main.rand.Next(-100, 101), target.Center.Y - 150f - (float)Main.rand.Next(30));
				Vector2 cometVel = CalamityUtils.CalculatePredictiveAimToTargetMaxUpdates(pos, target, 18f, 2);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), cometPos, cometVel, ModContent.ProjectileType<LeonidCometSmall>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, 0f, -1f);
			}
		}
	}
}
