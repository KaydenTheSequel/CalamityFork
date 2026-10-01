using System;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.DataStructures;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class ChainedMeatHook : ModProjectile, ILocalizedModType, IModType
{
	public const float MaxTwirlTime = 30f;

	private Particle smear;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/Melee/TrueBiomeBlade_LamentationsOfTheChainedHook";

	public Player Owner => Main.player[base.Projectile.owner];

	public float Timer => 30f - (float)base.Projectile.timeLeft;

	public ref float Twirling => ref base.Projectile.ai[0];

	public bool MayIExist
	{
		get
		{
			if (Owner.active && Owner.HeldItem.ModItem is OmegaBiomeBlade { secondaryAttunement: not null } blade)
			{
				return blade.secondaryAttunement.id == AttunementID.FlailBlade;
			}
			return false;
		}
	}

	public override void SetDefaults()
	{
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.width = (base.Projectile.height = 66);
		base.Projectile.tileCollide = false;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 40;
		base.Projectile.timeLeft = 1;
	}

	public override bool? CanHitNPC(NPC target)
	{
		return Twirling == 1f && !target.friendly;
	}

	public override void AI()
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Unknown result type (might be due to invalid IL or missing references)
		//IL_0346: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_038f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0394: Unknown result type (might be due to invalid IL or missing references)
		//IL_03af: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0403: Unknown result type (might be due to invalid IL or missing references)
		//IL_040e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0422: Unknown result type (might be due to invalid IL or missing references)
		//IL_0436: Unknown result type (might be due to invalid IL or missing references)
		//IL_044f: Unknown result type (might be due to invalid IL or missing references)
		//IL_045f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0464: Unknown result type (might be due to invalid IL or missing references)
		//IL_047c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0475: Unknown result type (might be due to invalid IL or missing references)
		if (!MayIExist)
		{
			base.Projectile.Kill();
			return;
		}
		Vector2 center;
		if (Twirling == 0f)
		{
			base.Projectile.velocity = Vector2.Zero;
			base.Projectile.timeLeft = 2;
			Vector2 val = Owner.Center - (float)Owner.direction * Vector2.UnitX * 40f;
			Vector2 one = Vector2.One;
			double radians = Main.GlobalTimeWrappedHourly;
			center = default(Vector2);
			Vector2 idealPosition = val + one.RotatedBy(radians, center) * 10f;
			center = idealPosition - base.Projectile.Center;
			if (((Vector2)(ref center)).Length() > 1500f)
			{
				base.Projectile.Center = idealPosition;
			}
			base.Projectile.Center = Vector2.Lerp(base.Projectile.Center, idealPosition, 0.05f);
			base.Projectile.Center = base.Projectile.Center.MoveTowards(idealPosition, 4f);
		}
		if (Twirling != 1f)
		{
			return;
		}
		if ((float)base.Projectile.timeLeft == 30f)
		{
			base.Projectile.velocity = Owner.SafeDirectionTo(Owner.Calamity().mouseWorld, Vector2.Zero);
			SoundEngine.PlaySound(in SoundID.Item71, base.Projectile.Center);
			for (int i = 0; i < 4; i++)
			{
				GeneralParticleHandler.SpawnParticle(new CritSpark(base.Projectile.Center, Main.rand.NextVector2Circular(1f, 1f) * Main.rand.NextFloat(17.5f, 25f) * base.Projectile.scale, Color.White, Main.rand.NextBool() ? Color.Cyan : Color.MediumTurquoise, 0.4f + Main.rand.NextFloat(0f, 0.5f), 20 + Main.rand.Next(30), 1f, 3f));
			}
			if (smear == null)
			{
				smear = new CircularSmearVFX(Owner.Center, Color.HotPink, base.Projectile.velocity.ToRotation(), base.Projectile.scale * 1.5f);
				GeneralParticleHandler.SpawnParticle(smear);
			}
		}
		Projectile projectile = base.Projectile;
		Vector2 velocity = base.Projectile.velocity;
		center = default(Vector2);
		projectile.velocity = velocity.RotatedBy(-0.41887906193733215, center);
		((Vector2)(ref base.Projectile.velocity)).Normalize();
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		base.Projectile.Center = Owner.Center + base.Projectile.velocity * 140f * (1f + 0.3f * (float)Math.Sin((float)base.Projectile.timeLeft / 30f * (float)Math.PI));
		if (smear != null)
		{
			smear.Rotation = base.Projectile.rotation + (float)Math.PI / 2f;
			smear.Time = 0;
			smear.Position = Owner.Center;
			smear.Scale = 3.4f;
			smear.Color = Color.MediumTurquoise * 0.9f * (float)Math.Sin((float)base.Projectile.timeLeft / 30f * (float)Math.PI);
		}
		if (base.Projectile.timeLeft == 1)
		{
			SoundEngine.PlaySound(in SoundID.Item101, base.Projectile.Center);
			for (int j = 0; j < 4; j++)
			{
				GeneralParticleHandler.SpawnParticle(new CritSpark(base.Projectile.Center, Main.rand.NextVector2Circular(1f, 1f) * Main.rand.NextFloat(17.5f, 25f) * base.Projectile.scale, Color.White, Main.rand.NextBool() ? Color.Cyan : Color.MediumTurquoise, 0.4f + Main.rand.NextFloat(0f, 0.5f), 20 + Main.rand.Next(30), 1f, 3f));
			}
			Twirling = 0f;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<GlacialState>(), 100);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item105, base.Projectile.Center);
		for (int i = 0; i < 10; i++)
		{
			GeneralParticleHandler.SpawnParticle(new CritSpark(base.Projectile.Center, Main.rand.NextVector2Circular(1f, 1f) * Main.rand.NextFloat(17.5f, 25f) * base.Projectile.scale, Color.White, Main.rand.NextBool() ? Color.Cyan : Color.MediumTurquoise, 0.4f + Main.rand.NextFloat(0f, 1.5f), 20 + Main.rand.Next(30), 1f, 3f));
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		Texture2D handle = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/TrueBiomeBlade_LamentationsOfTheChainedHookBody", (AssetRequestMode)2).Value;
		Texture2D blade = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/TrueBiomeBlade_LamentationsOfTheChainedHook", (AssetRequestMode)2).Value;
		float drawRotation = (base.Projectile.Center - Owner.Center).ToRotation() + (float)Math.PI / 4f;
		Vector2 drawOrigin = default(Vector2);
		((Vector2)(ref drawOrigin))._002Ector(0f, (float)handle.Height);
		Vector2 drawOffset = base.Projectile.Center - Main.screenPosition;
		Main.EntitySpriteDraw(handle, drawOffset, null, lightColor, drawRotation, drawOrigin, base.Projectile.scale, (SpriteEffects)0);
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		((Vector2)(ref drawOrigin))._002Ector(0f, (float)blade.Height);
		Main.EntitySpriteDraw(blade, drawOffset, null, Color.Lerp(Color.White, lightColor, 0.5f) * 0.9f, drawRotation, drawOrigin, base.Projectile.scale, (SpriteEffects)0);
		DrawChain();
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		return false;
	}

	public void DrawChain()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		Texture2D chainTex = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/TrueBiomeBlade_LamentationsOfTheChainedChain", (AssetRequestMode)2).Value;
		int dist = (int)Vector2.Distance(Owner.Center, base.Projectile.Center) / 16;
		Vector2[] Nodes = (Vector2[])(object)new Vector2[dist + 1];
		Nodes[0] = Owner.Center;
		Nodes[dist] = base.Projectile.Center;
		Rectangle frame = default(Rectangle);
		Vector2 scale = default(Vector2);
		Vector2 origin = default(Vector2);
		for (int i = 1; i < dist + 1; i++)
		{
			((Rectangle)(ref frame))._002Ector(0, 18 * (i % 2), 12, 18);
			Vector2 positionAlongLine = Vector2.Lerp(Owner.Center, base.Projectile.Center, (float)i / (float)dist);
			Nodes[i] = positionAlongLine;
			float rotation = (Nodes[i] - Nodes[i - 1]).ToRotation() - (float)Math.PI / 2f;
			float yScale = Vector2.Distance(Nodes[i], Nodes[i - 1]) / (float)frame.Height;
			((Vector2)(ref scale))._002Ector(1f, yScale);
			((Vector2)(ref origin))._002Ector((float)(frame.Width / 2), (float)frame.Height);
			Main.EntitySpriteDraw(chainTex, Nodes[i] - Main.screenPosition, frame, Color.White * 0.5f, rotation, origin, scale, (SpriteEffects)0);
		}
	}
}
