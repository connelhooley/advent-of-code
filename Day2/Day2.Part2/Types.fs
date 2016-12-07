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

type State = {
    current: Coordinate;
    result : list<int>;
}