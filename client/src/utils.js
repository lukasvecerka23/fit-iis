export const KpiFunction = {
    Less: 0,
    LessOrEqual: 1,
    Equal: 2,
    GreaterOrEqual: 3,
    Greater: 4,
    NotEqual: 5
};

export const ParameterStatus = {
    Okay: 0,
    Warning: 1,
    Critical: 2
}

export const DeviceStatus = {
    None: 0,
    Okay: 1,
    Warning: 2,
    Critical: 3
}

export const SystemStatus = {
    None: 0,
    Okay: 1,
    Warning: 2,
    Critical: 3
}

export function mapKpiFunctionToString(value) {
    const mapping = {
        [KpiFunction.Less]: "<",
        [KpiFunction.LessOrEqual]: "≤",
        [KpiFunction.Equal]: "=",
        [KpiFunction.GreaterOrEqual]: "≥",
        [KpiFunction.Greater]: ">",
        [KpiFunction.NotEqual]: "≠"
    };
    return mapping[value] || "-";
}