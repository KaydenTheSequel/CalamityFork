using System;
using System.Collections.Generic;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.NPCs;

public class CalamityPolarityNPC : GlobalNPC
{
	private float curPolarity;

	public List<Particle> pulses;

	private List<Particle> pulsesToClear;

	public bool isPolarized => curPolarity != 0f;

	public override bool InstancePerEntity => true;

	public float CurPolarity
	{
		get
		{
			return curPolarity;
		}
		internal set
		{
			curPolarity = value;
		}
	}

	public override void SetDefaults(NPC npc)
	{
		CurPolarity = 0f;
		pulses = new List<Particle>();
		pulsesToClear = new List<Particle>();
	}

	public void applyPolarity(float update, NPC npc)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		CurPolarity = (float)Math.Round(update);
		Color pulseColor = AdamantiteParticleAccelerator.LightColors[(update < 0f) ? 1u : 0u];
		pulses.Add(new AuraPulseRing(pulseColor, new Vector2(Math.Max((float)npc.width / 156f * 1.1f, 0.25f), 0.3f), new Vector2(Math.Max((float)npc.width / 156f * 1.5f, 0.4f), 0.01f), 40, npc));
	}

	public override GlobalNPC Clone(NPC npc, NPC npcClone)
	{
		CalamityPolarityNPC obj = (CalamityPolarityNPC)base.Clone(npc, npcClone);
		obj.curPolarity = CurPolarity;
		return obj;
	}

	public override void PostAI(NPC npc)
	{
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		HandlePulses();
		if (isPolarized)
		{
			if (curPolarity < 0f)
			{
				curPolarity++;
			}
			else if (curPolarity > 0f)
			{
				curPolarity--;
			}
			if (Main.rand.NextBool())
			{
				Color sparkColor = AdamantiteParticleAccelerator.LightColors[(CurPolarity < 0f) ? 1u : 0u];
				GeneralParticleHandler.SpawnParticle(new ElectricSpark(npc.Center + Main.rand.NextVector2Circular((float)npc.width / 2f, (float)npc.height / 2f), Main.rand.NextVector2CircularEdge(20f, 20f) * 0.4f, Color.Lerp(Color.White, sparkColor, 0.4f), sparkColor, Main.rand.NextFloat(0.5f, 1.2f), Main.rand.Next(20, 40), (float)Math.PI / 4f, 10f, 1f, 2.5f));
			}
		}
	}

	public void HandlePulses()
	{
		foreach (Particle pulse in pulses)
		{
			pulse.Time++;
			pulse.Update();
			if (pulse.Time > pulse.Lifetime)
			{
				pulsesToClear.Add(pulse);
			}
		}
		pulses.RemoveAll((Particle n) => pulsesToClear.Contains(n));
		pulsesToClear.Clear();
	}

	public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		if (CalamityDrawParameterNPC.DrawingMiracleBlight[npc.whoAmI])
		{
			return;
		}
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		foreach (Particle pulse in pulses)
		{
			pulse.CustomDraw(spriteBatch, npc.Center);
			pulse.CustomDraw(spriteBatch, npc.Center);
		}
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
	}
}
