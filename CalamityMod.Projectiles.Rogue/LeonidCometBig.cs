using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Weapons.Rogue;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class LeonidCometBig : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 50;
		base.Projectile.height = 42;
		base.Projectile.aiStyle = 1;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.tileCollide = false;
		base.Projectile.extraUpdates = 2;
		base.AIType = 424;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 240);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 240);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], CalamityUtils.ColorSwap(LeonidProgenitor.blueColor, LeonidProgenitor.purpleColor, 1f));
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
		Texture2D tex = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Rogue/LeonidCometBigGlow", (AssetRequestMode)2).Value;
		Main.EntitySpriteDraw(tex, base.Projectile.Center - Main.screenPosition, null, Color.White, base.Projectile.rotation, tex.Size() / 2f, base.Projectile.scale, (SpriteEffects)0);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0406: Unknown result type (might be due to invalid IL or missing references)
		//IL_042c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0436: Unknown result type (might be due to invalid IL or missing references)
		//IL_043b: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item89, base.Projectile.position);
		base.Projectile.position.X += base.Projectile.width / 2;
		base.Projectile.position.Y += base.Projectile.height / 2;
		base.Projectile.width = (int)(128f * base.Projectile.scale);
		base.Projectile.height = (int)(128f * base.Projectile.scale);
		base.Projectile.position.X -= base.Projectile.width / 2;
		base.Projectile.position.Y -= base.Projectile.height / 2;
		for (int index = 0; index < 8; index++)
		{
			Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 31, 0f, 0f, 100, CalamityUtils.ColorSwap(LeonidProgenitor.blueColor, LeonidProgenitor.purpleColor, 1f), 1.5f);
		}
		for (int i = 0; i < 32; i++)
		{
			int index2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 6, 0f, 0f, 100, CalamityUtils.ColorSwap(LeonidProgenitor.blueColor, LeonidProgenitor.purpleColor, 1f), 2.5f);
			Dust obj = Main.dust[index2];
			obj.noGravity = true;
			obj.velocity *= 3f;
			int index3 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 6, 0f, 0f, 100, CalamityUtils.ColorSwap(LeonidProgenitor.blueColor, LeonidProgenitor.purpleColor, 1f), 1.5f);
			Dust obj2 = Main.dust[index3];
			obj2.velocity *= 2f;
			obj2.noGravity = true;
		}
		if (!Main.dedServ)
		{
			for (int j = 0; j < 2; j++)
			{
				int index4 = Gore.NewGore(base.Projectile.GetSource_Death(), base.Projectile.position + new Vector2((float)(base.Projectile.width * Main.rand.Next(100)) / 100f, (float)(base.Projectile.height * Main.rand.Next(100)) / 100f) - Vector2.One * 10f, default(Vector2), Main.rand.Next(61, 64));
				Gore obj3 = Main.gore[index4];
				obj3.velocity *= 0.3f;
				obj3.velocity.X += (float)Main.rand.Next(-10, 11) * 0.05f;
				obj3.velocity.Y += (float)Main.rand.Next(-10, 11) * 0.05f;
			}
		}
		if (base.Projectile.owner == Main.myPlayer)
		{
			base.Projectile.usesLocalNPCImmunity = true;
			base.Projectile.localNPCHitCooldown = 10;
			base.Projectile.localAI[1] = -1f;
			base.Projectile.maxPenetrate = 0;
			base.Projectile.Damage();
		}
		for (int k = 0; k < 5; k++)
		{
			int index5 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, Utils.SelectRandom<int>(Main.rand, 6, 259, 158), 2.5f * (float)base.Projectile.direction, -2.5f, 0, CalamityUtils.ColorSwap(LeonidProgenitor.blueColor, LeonidProgenitor.purpleColor, 1f));
			Dust obj4 = Main.dust[index5];
			obj4.alpha = 200;
			obj4.velocity *= 2.4f;
			obj4.scale += Main.rand.NextFloat();
		}
	}
}
