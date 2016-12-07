module Types

type Direction =
| Up
| Right
| Down
| Left

type Instruction =
| Direction of Direction
| Read

type Coordinate = {
    x: int;
    y: int;
}

type Button = {
    coordinate: Coordinate;
    value: string;
}

type State = {
    current: Button;
    result : list<string>;
}