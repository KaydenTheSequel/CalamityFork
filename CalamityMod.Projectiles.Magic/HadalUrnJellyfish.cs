using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class HadalUrnJellyfish : ModProjectile, ILocalizedModType, IModType
{
	private bool neartarget;

	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 5;
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 38;
		base.Projectile.height = 38;
		base.Projectile.friendly = true;
		base.Projectile.timeLeft = 600;
		base.Projectile.penetrate = 3;
		base.Projectile.alpha = 255;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 16;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
	}

	public override void AI()
	{
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0310: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0369: Unknown result type (might be due to invalid IL or missing references)
		//IL_036e: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.ai[0]++;
		base.Projectile.ai[1]--;
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 8)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= Main.projFrames[base.Type])
		{
			base.Projectile.frame = 0;
		}
		if (base.Projectile.timeLeft > 60)
		{
			if (base.Projectile.alpha > 0)
			{
				base.Projectile.alpha -= 25;
			}
			if (base.Projectile.alpha < 0)
			{
				base.Projectile.alpha = 0;
			}
		}
		else
		{
			base.Projectile.alpha += 4;
			if (base.Projectile.alpha >= 255)
			{
				base.Projectile.active = false;
			}
		}
		if (base.Projectile.ai[0] % 90f == 0f && base.Projectile.ai[1] <= 0f)
		{
			CalamityUtils.HomeInOnNPC(base.Projectile, ignoreTiles: true, 1200f, 50f, 20f);
		}
		else
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.985f;
		}
		int maxDistance = 10;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC n = enumerator.Current;
			if (n.CanBeChasedBy(base.Projectile))
			{
				float extraDistance = n.width / 2 + n.height / 2;
				bool canHit = true;
				if (extraDistance < (float)maxDistance)
				{
					canHit = Collision.CanHit(base.Projectile.Center, 1, 1, n.Center, 1, 1);
				}
				if (base.Projectile.WithinRange(n.Center, (float)maxDistance + extraDistance) & canHit)
				{
					neartarget = true;
				}
			}
		}
		if (neartarget && base.Projectile.ai[1] <= 0f)
		{
			base.Projectile.ExpandHitboxBy(base.Projectile.width * 2, base.Projectile.height * 2);
			base.Projectile.ai[1] = 120f;
		}
		if (base.Projectile.ai[1] > 80f)
		{
			Projectile projectile2 = base.Projectile;
			projectile2.velocity *= 0.7f;
			if (base.Projectile.ai[1] == 110f)
			{
				BloomRing bloomRing = new BloomRing(base.Projectile.Center, Vector2.Zero, Color.Aqua * 0.5f, base.Projectile.scale / 2f, 40);
				GeneralParticleHandler.SpawnParticle(bloomRing);
				bloomRing.Position = base.Projectile.Center;
				StrongBloom strongBloom = new StrongBloom(base.Projectile.Center, Vector2.Zero, Color.Aqua * 0.3f, base.Projectile.scale * (1f + Main.rand.NextFloat(0f, 1.5f)) / 2f, 40);
				GeneralParticleHandler.SpawnParticle(strongBloom);
				strongBloom.Position = base.Projectile.Center;
				base.Projectile.penetrate = -1;
				base.Projectile.localNPCHitCooldown = 10;
			}
		}
		if (base.Projectile.ai[1] == 80f)
		{
			base.Projectile.ExpandHitboxBy(base.Projectile.width / 2, base.Projectile.height / 2);
			base.Projectile.localNPCHitCooldown = 16;
			base.Projectile.timeLeft = 60;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<CrushDepth>(), 240);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		int textureheight = tex.Height / Main.projFrames[base.Type];
		int y = textureheight * base.Projectile.frame;
		Main.EntitySpriteDraw(tex, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)new Rectangle(0, y, tex.Width, textureheight), base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, new Vector2((float)tex.Width / 2f, (float)textureheight / 2f), base.Projectile.scale, (SpriteEffects)0, 0f);
		return false;
	}
}
