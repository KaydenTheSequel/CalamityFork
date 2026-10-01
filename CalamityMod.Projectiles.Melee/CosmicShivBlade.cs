using System;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class CosmicShivBlade : ModProjectile, ILocalizedModType, IModType
{
	public Vector2 StartPosition;

	public int ColorIndex;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 15;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 42;
		base.Projectile.height = 42;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 5;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 6000;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.extraUpdates = 3;
		base.Projectile.alpha = 0;
		base.Projectile.scale = 1.2f;
	}

	public override void OnSpawn(IEntitySource source)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		StartPosition = base.Projectile.position;
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 4f;
		ColorIndex = Main.rand.Next(0, CosmicShivTrail.DustColors.Count);
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 4f;
		if (base.Projectile.Distance(StartPosition) > base.Projectile.ai[0] * 2f)
		{
			base.Projectile.Kill();
		}
		Vector2 center = base.Projectile.Center;
		Color alpha = base.Projectile.GetAlpha(Color.White);
		Lighting.AddLight(center, ((Color)(ref alpha)).ToVector3() * 0.2f);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<GodSlayerInferno>(), 120);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Rectangle sourceRectangle = texture.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		Vector2 origin = sourceRectangle.Size() / 2f;
		float rotation = base.Projectile.rotation;
		Color randomColor = base.Projectile.GetAlpha(lightColor);
		float scaleMult = 1.2f;
		for (int i = 0; i < base.Projectile.oldPos.Length; i++)
		{
			Vector2 drawPos = base.Projectile.oldPos[i] + base.Projectile.Size / 2f - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY);
			Color color = randomColor * ((float)(base.Projectile.oldPos.Length - i) / (float)base.Projectile.oldPos.Length);
			Main.spriteBatch.Draw(texture, drawPos, (Rectangle?)sourceRectangle, color, rotation, origin, base.Projectile.scale * scaleMult, (SpriteEffects)0, 0f);
			Main.spriteBatch.Draw(texture, drawPos, (Rectangle?)sourceRectangle, color * 0.5f, rotation, origin, base.Projectile.scale * 1.6f * scaleMult, (SpriteEffects)0, 0f);
			scaleMult *= 0.95f;
		}
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 6; i++)
		{
			Dust.NewDustDirect(base.Projectile.Center, base.Projectile.width, base.Projectile.height, 267, 0f, 0f, 0, base.Projectile.GetAlpha(Color.Purple) * 0.4f, 1.5f).noGravity = true;
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		Color color = CosmicShivTrail.DustColors[ColorIndex] * base.Projectile.Opacity;
		color = Color.Lerp(color, Color.White, 0.6f);
		((Color)(ref color)).A = 0;
		return color;
	}

	public CosmicShivBlade()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		StartPosition = Vector2.Zero;
		base._002Ector();
	}
}
