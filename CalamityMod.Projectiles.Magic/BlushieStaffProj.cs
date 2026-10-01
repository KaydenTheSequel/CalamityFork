using CalamityMod.NPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

[PierceResistException(false)]
public class BlushieStaffProj : ModProjectile, ILocalizedModType, IModType
{
	private const int xRange = 600;

	private const int yRange = 320;

	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 2;
		base.Projectile.height = 2;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = DamageClass.Magic;
	}

	public override void AI()
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		if (Main.myPlayer == base.Projectile.owner && (player.CantUseHoldout() || base.Projectile.ai[0] > 3600f))
		{
			base.Projectile.Kill();
		}
		base.Projectile.Center = player.MountedCenter;
		base.Projectile.timeLeft = 2;
		player.itemTime = 2;
		player.itemAnimation = 2;
		base.Projectile.ai[0]++;
		base.Projectile.damage = (int)(base.Projectile.ai[0] - 120f);
		if (base.Projectile.damage >= 100 && Main.myPlayer == base.Projectile.owner)
		{
			if (player.statMana <= 0 && player.manaFlower)
			{
				player.QuickMana();
			}
			if (player.statMana > 0)
			{
				player.statMana--;
			}
			else
			{
				base.Projectile.Kill();
			}
		}
		if (base.Projectile.localAI[1] == 0f)
		{
			base.Projectile.localAI[1] = 37f;
			base.Projectile.localAI[0] = Main.rand.Next(600);
		}
		base.Projectile.localAI[0] = Next(base.Projectile.localAI[0]);
	}

	private float Next(float seed)
	{
		return (seed * base.Projectile.localAI[1] + 101f) % 768000f;
	}

	public override bool? CanDamage()
	{
		return base.Projectile.ai[0] > 120f;
	}

	public override void ModifyDamageHitbox(ref Rectangle hitbox)
	{
		hitbox.X -= 600;
		hitbox.Width += 1200;
		hitbox.Y -= 320;
		hitbox.Height += 640;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		modifiers.DefenseEffectiveness *= 0f;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		base.Projectile.penetrate++;
		target.AddBuff(189, 300);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_033b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0351: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0376: Unknown result type (might be due to invalid IL or missing references)
		//IL_038c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0398: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03df: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_049b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0417: Unknown result type (might be due to invalid IL or missing references)
		//IL_043b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0440: Unknown result type (might be due to invalid IL or missing references)
		//IL_0445: Unknown result type (might be due to invalid IL or missing references)
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.NonPremultiplied, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		Vector2 center = base.Projectile.Center - Main.screenPosition;
		Vector2 aura = center + new Vector2(-600f, 320f);
		Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Magic/BlushieStaffAura", (AssetRequestMode)2).Value;
		Rectangle frame = default(Rectangle);
		((Rectangle)(ref frame))._002Ector(50, 0, 50, 32);
		int count = 24;
		SpriteEffects effects = (SpriteEffects)(!(base.Projectile.ai[0] % 30f < 15f));
		for (int k = 0; k < count; k++)
		{
			if (k == 0)
			{
				frame.X = (((int)effects != 0) ? 100 : 0);
			}
			else if (k == count - 1)
			{
				frame.X = (((int)effects == 0) ? 100 : 0);
			}
			else
			{
				frame.X = 50;
			}
			Main.EntitySpriteDraw(texture, aura, frame, Color.White, 0f, new Vector2(0f, 32f), 1f, effects);
			aura.X += 50f;
		}
		Vector2 topLeftGear = center + new Vector2(-400f, -100f);
		Vector2 topRightGear = center + new Vector2(200f, -200f);
		Vector2 bottomLeftGear = center + new Vector2(-300f, 160f);
		Vector2 bottomRightGear = center + new Vector2(500f, 220f);
		float alpha = base.Projectile.ai[0] / 60f;
		if (alpha > 0f)
		{
			if (alpha > 1f)
			{
				alpha = 1f;
			}
			DrawChains(topLeftGear, center, alpha);
			DrawChains(center, bottomLeftGear, alpha);
			DrawChains(bottomLeftGear, topLeftGear, alpha);
			DrawChains(center, bottomRightGear, alpha);
			DrawChains(bottomRightGear, topRightGear, alpha);
			DrawChains(topRightGear, center, alpha);
			DrawChains(new Vector2(bottomLeftGear.X, center.Y + 320f), bottomLeftGear, alpha);
			DrawChains(bottomRightGear, new Vector2(bottomRightGear.X, center.Y + 320f), alpha);
		}
		float scale = 1f;
		if (base.Projectile.ai[0] < 60f)
		{
			scale = 4f - 3f * base.Projectile.ai[0] / 60f;
		}
		texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Magic/BlushieStaffGear", (AssetRequestMode)2).Value;
		Vector2 origin = default(Vector2);
		((Vector2)(ref origin))._002Ector(48f, 48f);
		Main.EntitySpriteDraw(texture, center, null, Color.White, base.Projectile.ai[0] / 20f, origin, 1.5f * scale, (SpriteEffects)0);
		Main.EntitySpriteDraw(texture, topLeftGear, null, Color.White, base.Projectile.ai[0] / 10f, origin, scale, (SpriteEffects)0);
		Main.EntitySpriteDraw(texture, topRightGear, null, Color.White, (0f - base.Projectile.ai[0]) / 8f, origin, 0.75f * scale, (SpriteEffects)0);
		Main.EntitySpriteDraw(texture, bottomLeftGear, null, Color.White, (0f - base.Projectile.ai[0]) / 15f, origin, 1.4f * scale, (SpriteEffects)0);
		Main.EntitySpriteDraw(texture, bottomRightGear, null, Color.White, base.Projectile.ai[0] / 10f, origin, scale, (SpriteEffects)0);
		float seed = base.Projectile.localAI[0];
		texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Magic/BlushieStaffFire", (AssetRequestMode)2).Value;
		Vector2 topLeft = center + new Vector2(-600f, -320f);
		for (int i = (int)base.Projectile.ai[0] - 60; i < (int)base.Projectile.ai[0]; i++)
		{
			if (i > 120)
			{
				Main.spriteBatch.Draw(texture, topLeft + new Vector2(seed % 1200f, seed % 640f + (float)i - base.Projectile.ai[0]), Color.White);
			}
			seed = Next(seed);
		}
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		return false;
	}

	private void DrawChains(Vector2 start, Vector2 end, float alpha)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Magic/BlushieStaffChain", (AssetRequestMode)2).Value;
		Vector2 unit = end - start;
		float num = ((Vector2)(ref unit)).Length() - 36f;
		((Vector2)(ref unit)).Normalize();
		float rotation = unit.ToRotation();
		start += unit * 18f;
		float offset = base.Projectile.ai[0] * 2f % (float)texture.Width;
		start += unit * offset;
		int count = (int)((num - offset) / (float)texture.Width);
		Color color = Color.White * alpha;
		Vector2 origin = default(Vector2);
		((Vector2)(ref origin))._002Ector((float)(texture.Width / 2), (float)(texture.Height / 2));
		for (int k = 0; k <= count; k++)
		{
			Main.EntitySpriteDraw(texture, start, null, color, rotation, origin, 1f, (SpriteEffects)0);
			start += unit * (float)texture.Width;
		}
	}
}
