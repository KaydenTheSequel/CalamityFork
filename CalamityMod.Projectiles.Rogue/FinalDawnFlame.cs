using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class FinalDawnFlame : ModProjectile, ILocalizedModType, IModType
{
	internal struct Flame
	{
		public Vector2 Position;

		public int FrameCounter;

		public int Frame;

		public float Alpha;

		public float Scale;

		public int Direction;
	}

	private Flame[] Flames;

	public const int TotalFlames = 120;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 8;
	}

	public override void SetDefaults()
	{
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.scale = 1f;
		base.Projectile.width = 1000;
		base.Projectile.height = 100;
		base.Projectile.penetrate = -1;
		base.Projectile.hostile = false;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.alpha = 255;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 30;
		Flames = new Flame[120];
		for (int i = 0; i < Flames.Length; i++)
		{
			Vector2 flamePosition = Main.rand.NextVector2Circular((float)base.Projectile.width * 0.36f, base.Projectile.height / 2);
			Flames[i].Position = flamePosition;
			Flames[i].Frame = Main.rand.Next(8);
			Flames[i].FrameCounter = Main.rand.Next(5);
			Flames[i].Alpha = 1f;
			Flames[i].Scale = 0.8f + 0.4f * Main.rand.NextFloat();
			Flames[i].Direction = Main.rand.Next(2);
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		int frameHeight = TextureAssets.Projectile[base.Type].Value.Height / Main.projFrames[base.Type];
		if (Flames.Length != 0)
		{
			for (int i = 0; i < Flames.Length; i++)
			{
				int frameHeight2 = frameHeight * Flames[i].Frame;
				Main.EntitySpriteDraw(texture, base.Projectile.Center + Flames[i].Position - Main.screenPosition, (Rectangle?)new Rectangle(0, frameHeight2, texture.Width, frameHeight), base.Projectile.GetAlpha(Color.White) * Flames[i].Alpha, base.Projectile.rotation, new Vector2((float)texture.Width / 2f, (float)frameHeight / 2f), Flames[i].Scale, (SpriteEffects)(Flames[i].Direction != 1), 0f);
			}
		}
		return false;
	}

	public override void AI()
	{
		if (Flames.Length != 0)
		{
			for (int i = 0; i < Flames.Length; i++)
			{
				AdjustFlameValues(ref Flames[i]);
			}
		}
		AlphaAdjustments();
	}

	internal void AdjustFlameValues(ref Flame flame)
	{
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		flame.FrameCounter++;
		if (flame.FrameCounter < 5)
		{
			return;
		}
		flame.Frame++;
		flame.FrameCounter = 0;
		if (flame.Frame >= 8)
		{
			if (base.Projectile.ai[0] < 570f)
			{
				Vector2 pos = Utils.RotatedByRandom(new Vector2(Main.rand.NextFloat((float)base.Projectile.width * 0.36f)), 6.2831854820251465);
				float widthHeightRatio = (float)base.Projectile.height / (float)base.Projectile.width;
				pos.Y *= widthHeightRatio;
				flame.Position = pos;
				flame.Scale = 0.8f + 0.4f * Main.rand.NextFloat();
				flame.Direction = Main.rand.Next(2);
				flame.Frame = 0;
			}
			else
			{
				flame.Alpha = 0f;
			}
		}
	}

	public void AlphaAdjustments()
	{
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] < 570f)
		{
			base.Projectile.alpha -= 10;
			if (base.Projectile.alpha < 0)
			{
				base.Projectile.alpha = 0;
			}
		}
		else
		{
			base.Projectile.friendly = false;
			base.Projectile.alpha += 5;
			if (base.Projectile.alpha > 255)
			{
				base.Projectile.alpha = 255;
			}
		}
		if (base.Projectile.ai[0] >= 600f && base.Projectile.alpha >= 255)
		{
			base.Projectile.Kill();
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<Dragonfire>(), 180);
	}
}
