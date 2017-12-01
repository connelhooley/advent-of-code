module Types

type Direction =
    | North
    | East
    | South
    | West

type Turn =
    | R
    | L

type Step =
    | Right
    | Left
    | Straight

type Position = {
    x: int;
    y: int;
}

type Traveling = {
    direction: Direction;
    position: Position;
    history: list<Position>
}

type Arrived = {
    position: Position;
}

type State = 
    | TravelingState of Traveling
    | ArrivedState of Arrived