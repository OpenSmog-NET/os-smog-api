﻿using OS.Smog.Interpreter;
using OS.Smog.Domain.Sensors.Expressions;

namespace OS.Smog.Domain.Sensors
{
    public static class PayloadInterpreter
    {
        private static readonly IExpression<PayloadInterpretationContext>[] Expressions = {
            new PayloadValidationExpression(),
            new TimeStampValidationExpression(),
            new HumidityValidationExpression(),
            new TempCValidationExpression(),
            new PressureValidationExpression(),
            new COExpression(),
            new PbExpression(),
            new NO2Expression(),
            new O3Expression(),
            new Pm10Expression(),
            new Pm25Expression(),
            new SO2Expression()
        };

        public static void Interpret(PayloadInterpretationContext context)
        {
            foreach (var expression in Expressions)
            {
                if (!expression.Interpret(context))
                {
                    break;
                }
            }
        }
    }
}