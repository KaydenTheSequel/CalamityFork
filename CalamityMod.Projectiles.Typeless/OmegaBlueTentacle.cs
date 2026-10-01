using CalamityMod.NPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

[PierceResistException(false)]
public class OmegaBlueTentacle : ModProjectile, ILocalizedModType, IModType
{
	public bool initSegments;

	public Vector2[] segment = (Vector2[])(object)new Vector2[6];

	public new string LocalizationCategory => "Projectiles.Typeless";

	private Player Owner => Main.player[base.Projectile.owner];

	public override void SetDefaults()
	{
		base.Projectile.width = 24;
		base.Projectile.height = 24;
		base.Projectile.timeLeft = 8;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
	}

	public override bool PreAI()
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		if (!initSegments)
		{
			initSegments = true;
			for (int i = 0; i < 6; i++)
			{
				segment[i] = base.Projectile.Center;
			}
		}
		return true;
	}

	public override void AI()
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0354: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_036d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0389: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_0411: Unknown result type (might be due to invalid IL or missing references)
		//IL_041c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0421: Unknown result type (might be due to invalid IL or missing references)
		//IL_0426: Unknown result type (might be due to invalid IL or missing references)
		//IL_0431: Unknown result type (might be due to invalid IL or missing references)
		//IL_043c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0441: Unknown result type (might be due to invalid IL or missing references)
		//IL_0446: Unknown result type (might be due to invalid IL or missing references)
		//IL_0448: Unknown result type (might be due to invalid IL or missing references)
		//IL_0450: Unknown result type (might be due to invalid IL or missing references)
		//IL_0455: Unknown result type (might be due to invalid IL or missing references)
		//IL_045a: Unknown result type (might be due to invalid IL or missing references)
		//IL_045c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0471: Unknown result type (might be due to invalid IL or missing references)
		//IL_0484: Unknown result type (might be due to invalid IL or missing references)
		//IL_048a: Unknown result type (might be due to invalid IL or missing references)
		//IL_048c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0491: Unknown result type (might be due to invalid IL or missing references)
		//IL_0496: Unknown result type (might be due to invalid IL or missing references)
		//IL_0498: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0519: Unknown result type (might be due to invalid IL or missing references)
		//IL_0520: Unknown result type (might be due to invalid IL or missing references)
		//IL_0525: Unknown result type (might be due to invalid IL or missing references)
		//IL_04da: Unknown result type (might be due to invalid IL or missing references)
		//IL_04de: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_052c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0530: Unknown result type (might be due to invalid IL or missing references)
		//IL_053c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0565: Unknown result type (might be due to invalid IL or missing references)
		//IL_056a: Unknown result type (might be due to invalid IL or missing references)
		//IL_056f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0574: Unknown result type (might be due to invalid IL or missing references)
		//IL_0579: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		bool madness = Owner.Calamity().omegaBlueAbyssalMadness;
		if (Owner.active && Owner.Calamity().omegaBlueSet)
		{
			base.Projectile.timeLeft = 8;
		}
		Vector2 playerVel = Owner.position - Owner.oldPosition;
		Projectile projectile = base.Projectile;
		projectile.position += playerVel;
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] >= 0f)
		{
			Vector2 distance = Owner.Center + Utils.RotatedBy(new Vector2(50f, 0f), (double)(MathHelper.ToRadians(60f) * base.Projectile.ai[1]), default(Vector2)) - base.Projectile.Center;
			float range = ((Vector2)(ref distance)).Length();
			((Vector2)(ref distance)).Normalize();
			if (base.Projectile.ai[0] == 0f)
			{
				if (range > 13f)
				{
					base.Projectile.ai[0] = -1f;
					if (range > 1300f)
					{
						base.Projectile.Kill();
						return;
					}
				}
				else
				{
					if (madness)
					{
						base.Projectile.ai[0] = 120f;
					}
					((Vector2)(ref base.Projectile.velocity)).Normalize();
					Projectile projectile2 = base.Projectile;
					projectile2.velocity *= 3f + Main.rand.NextFloat(3f);
					base.Projectile.netUpdate = true;
				}
			}
			else
			{
				distance /= 8f;
			}
			if (range > 120f)
			{
				base.Projectile.ai[0] = -1f;
				base.Projectile.netUpdate = true;
			}
			Projectile projectile3 = base.Projectile;
			projectile3.velocity += distance;
			if (range > 30f)
			{
				Projectile projectile4 = base.Projectile;
				projectile4.velocity *= 0.96f;
			}
			if (base.Projectile.ai[0] > 120f)
			{
				base.Projectile.ai[0] = 10 + Main.rand.Next(10);
				float maxDistance = (madness ? 900f : 600f);
				int target = -1;
				ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
				while (enumerator.MoveNext())
				{
					NPC npc = enumerator.Current;
					if (npc.CanBeChasedBy(base.Projectile))
					{
						float npcDistance = base.Projectile.Distance(npc.Center);
						if (npcDistance < maxDistance)
						{
							maxDistance = npcDistance;
							target = npc.whoAmI;
						}
					}
				}
				if (target != -1)
				{
					base.Projectile.velocity = Vector2.Normalize(Main.npc[target].Center - base.Projectile.Center) * 13f + Main.npc[target].velocity / 2f - playerVel / 2f;
					base.Projectile.ai[0] *= -1f;
				}
				base.Projectile.netUpdate = true;
			}
		}
		segment[0] = Owner.Center;
		for (int i = 1; i < 5; i++)
		{
			MoveSegment(segment[i - 1], ref segment[i], segment[i + 1]);
		}
		MoveSegment(segment[4], ref segment[5], base.Projectile.Center + base.Projectile.velocity);
		if (!madness)
		{
			return;
		}
		if (base.Projectile.ai[0] != -1f)
		{
			base.Projectile.ai[0]++;
		}
		Projectile projectile5 = base.Projectile;
		projectile5.position += base.Projectile.velocity;
		Vector2 dustPos = base.Projectile.position + base.Projectile.velocity;
		Vector2 tickVel = dustPos - base.Projectile.oldPosition;
		dustPos += Utils.RotatedBy(new Vector2((float)(base.Projectile.width / 2), 0f), (double)base.Projectile.rotation, default(Vector2));
		dustPos += new Vector2((float)(base.Projectile.width / 2 - 4), (float)(base.Projectile.height / 2 - 4));
		int limit = (int)(((Vector2)(ref tickVel)).Length() / 3f);
		if (limit == 0)
		{
			Dust dust = Dust.NewDustPerfect(dustPos, 20, Vector2.Zero, 100, Color.Transparent, 0.9f);
			dust.noGravity = true;
			dust.noLight = true;
			dust.fadeIn = 1f;
			return;
		}
		((Vector2)(ref tickVel)).Normalize();
		tickVel *= 3f;
		for (int j = 0; j <= limit; j++)
		{
			Dust dust2 = Dust.NewDustPerfect(dustPos, 20, Vector2.Zero, 100, Color.Transparent, 0.9f);
			dust2.noGravity = true;
			dust2.noLight = true;
			dust2.fadeIn = 1f;
			dust2.position -= tickVel * (float)j;
		}
	}

	private static void MoveSegment(Vector2 previous, ref Vector2 current, Vector2 next)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		current = previous + next;
		current /= 2f;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (Owner.Calamity().omegaBlueAbyssalMadness)
		{
			modifiers.SetCrit();
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		Owner.DoLifestealDirect(target, 10 * hit.Damage / base.Projectile.damage, 0.5f);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		GameShaders.Armor.ApplySecondary(Owner.cBody, Owner);
		Texture2D texture2D13 = TextureAssets.Projectile[base.Type].Value;
		Texture2D segmentSprite = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Typeless/OmegaBlueTentacleSegment1", (AssetRequestMode)2).Value;
		for (int i = 0; i < 5; i++)
		{
			base.Projectile.rotation = (base.Projectile.Center - segment[i]).ToRotation();
			switch (i)
			{
			case 1:
				segmentSprite = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Typeless/OmegaBlueTentacleSegment2", (AssetRequestMode)2).Value;
				break;
			case 2:
				segmentSprite = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Typeless/OmegaBlueTentacleSegment3", (AssetRequestMode)2).Value;
				break;
			case 3:
				segmentSprite = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Typeless/OmegaBlueTentacleSegment4", (AssetRequestMode)2).Value;
				break;
			case 4:
				segmentSprite = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Typeless/OmegaBlueTentacleSegment5", (AssetRequestMode)2).Value;
				break;
			}
			Main.spriteBatch.Draw(segmentSprite, segment[i] - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)segmentSprite.Bounds, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, segmentSprite.Bounds.Size() / 2f, base.Projectile.scale, (SpriteEffects)0, 0f);
		}
		base.Projectile.rotation = (base.Projectile.Center - segment[5]).ToRotation();
		Main.spriteBatch.Draw(texture2D13, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)texture2D13.Bounds, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, texture2D13.Bounds.Size() / 2f, base.Projectile.scale, (SpriteEffects)0, 0f);
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		return false;
	}
}
